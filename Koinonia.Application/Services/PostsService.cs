using Koinonia.Application.Interface;
using Koinonia.Application.ViewModels.Comments;
using Koinonia.Application.ViewModels.Posts;
using Koinonia.Domain.Interface;
using Koinonia.Domain.Models;
using Koinonia.Infra.Data.Context;
using Koinonia.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koinonia.Application.Services
{
    public class PostsService : Repository<Posts>, IPostsService
    {
        private readonly IRepository<Posts> postRepo;

        public PostsService(KoinoniaDbContext context, IRepository<Posts> PostRepo) : base(context)
        {
            postRepo = PostRepo;
        }
        public async Task<Posts> AddNewUserPost(PostsViewModel model, string fileName)
        {
           //create a new instance of the entity post

            Posts userPost = new Posts()
            {
                Visibility = model.VisibilityStatus,
                DatePosted = model.DatePosted,
                Content = model.Content,
                ImageFileName = fileName,
                PostCategory = model.PostCategory,
                UserId = model.userId
            };
            var added = await postRepo.AddNewAsync(userPost);
            if(added != null)
            {
                await postRepo.SaveChangesAsync();
                return added;
            }
            return null;
        }

        public void DeletePost(Guid PostId)
        {
            postRepo.Delete(PostId);
            postRepo.SaveChanges(); 
        }

        public IQueryable<Posts> GetAllChurchNews()
        {
            var ChurchNews = _context.Post
                .Where(x => x.PostCategory == Category.News)
                .Include(x => x.User)
                .Include(x => x.PostLikes)
                .Include(x => x.PostComments);

            return ChurchNews;
        }

        public IQueryable<Posts> GetAllTestimonies()
        {
            var Testimonies = _context.Post
                .Where(x => x.PostCategory == Category.Testimony)
                .Include(x => x.User)
                .Include(x => x.PostLikes)
                .Include(x => x.PostComments);

            return Testimonies;
        }

        public IQueryable<Posts> GetAllUserStories()
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

        public async Task<Posts> UpdatePost(PostsViewModel model)
        {
            //fetch the old post
            var post = postRepo.Get(model.PostId);

            //update post properties
            post.Content = model.Content;
            post.DatePosted = DateTime.Now;
            post.ImageFileName = model.ExistingPhotoPath;
            post.PostCategory = model.PostCategory;
            post.Visibility = model.VisibilityStatus;
            post.UserId = model.userId;

            postRepo.Update(post);
            await postRepo.SaveChangesAsync();
            return post;
        }
    }
}
