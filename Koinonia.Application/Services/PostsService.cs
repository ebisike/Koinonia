using Koinonia.Application.Interface;
using Koinonia.Application.ViewModels.Comments;
using Koinonia.Application.ViewModels.Posts;
using Koinonia.Domain.Interface;
using Koinonia.Domain.Models;
using Koinonia.Infra.Data.Context;
using Koinonia.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koinonia.Application.Services
{
    public class PostsService : Repository<Posts>, IPostsService
    {
        private readonly IRepository<Posts> postRepo;
        public int MaxTake { get; set; } = 100;

        public PostsService(KoinoniaDbContext context, IRepository<Posts> PostRepo) : base(context)
        {
            postRepo = PostRepo;
        }
        public async Task<Posts> AddNewUserPost(PostsViewModel model, List<string> fileName)
        {
            //create a new instance of the entity post

            Posts userPost = new Posts()
            {
                Id = Guid.NewGuid(),
                Visibility = model.VisibilityStatus,
                DatePosted = model.DatePosted,
                Content = model.Content,                
                PostCategory = model.PostCategory,
                UserId = model.userId
            };
            var postAdded = await postRepo.AddNewAsync(userPost);

            //add each media file to db
            foreach (var item in fileName)
            {
                MediaFiles media = new MediaFiles()
                {
                    PostId = postAdded.Id,
                    fileName = item
                };
                _context.MediaFiles.Add(media);
            }
            if(postAdded != null)
            {
                await _context.SaveChangesAsync();
                return postAdded;
            }
            return null;
        }

        public void DeletePost(Guid PostId)
        {
            postRepo.Delete(PostId);
            postRepo.SaveChanges(); 
        }

        public IQueryable<Posts> GetAllChurchNews(int size)
        {
            var ChurchNews = _context.Post
                .Skip((size - 1) * MaxTake)
                .Take(MaxTake)
                .Where(x => x.PostCategory == Category.News)
                .Include(x => x.User)
                .Include(x => x.PostLikes)
                .Include(x => x.PostComments)
                .OrderByDescending(t => t.DatePosted.ToShortTimeString());

            return ChurchNews;
        }

        public IQueryable<Posts> GetAllTestimonies(int size)
        {
            var Testimonies = _context.Post
                .Where(x => x.PostCategory == Category.Testimony)
                .Include(x => x.User)
                .Include(x => x.PostLikes)
                .Include(x => x.PostComments)
                .OrderByDescending(t => t.DatePosted.ToShortTimeString());

            return Testimonies;
        }

        public IQueryable<Posts> GetAllUserStories(int size)
        {
            var UserStories = _context.Post
                .Where(x => x.PostCategory == Category.UserStories)
                .Include(u => u.User)
                .Include(l => l.PostLikes)
                .Include(c => c.PostComments);

            return UserStories;
        }

        public async Task<Posts> GetPost(Guid PostId)
        {
            Posts post = await _context.Post
                .Where(p => p.Id == PostId)
                .Include(u => u.User)
                .Include(l => l.PostLikes)
                .Include(c => c.PostComments)
                .FirstOrDefaultAsync();

            return post;
        }

        public PostCommentsViewModel GetPostComments(Guid PostId)
        {
            throw new NotImplementedException();
        }

        public async Task<Posts> UpdatePost(PostsViewModel model, List<string> fileName)
        {
            //fetch the old post
            var post = postRepo.Get(model.PostId);

            //update post properties
            post.Content = string.IsNullOrWhiteSpace(model.Content) ? post.Content : model.Content;
            post.PostCategory = model.PostCategory;
            post.Visibility = model.VisibilityStatus;
            post.DatePosted = post.DatePosted;
            post.UserId = model.userId;

            postRepo.Update(post);

            foreach (var item in fileName)
            {
                MediaFiles media = new MediaFiles()
                {
                    PostId = post.Id,
                    fileName = item,
                };
                _context.MediaFiles.Update(media);
            }
            await _context.SaveChangesAsync();
            return post;
        }
    }
}
