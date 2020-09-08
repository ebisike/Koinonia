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

        private KoinoniaUsers koinoniaUsers;

        //private readonly KoinoniaDbContext _context;

        public PostsService(KoinoniaDbContext context, IRepository<Posts> PostRepo) : base(context)
        {
            postRepo = PostRepo;
        }
        public async Task<Posts> AddNewUserPost(PostsViewModel model, string fileName, Guid userId)
        {
            //fetch the koinoniaUser id
            //koinoniaUsers = applicationUser.GetApplicationUser(userId);

            //throw new NotImplementedException();
            //create a new instance of the entity post

            Posts userPost = new Posts()
            {
                Visibility = model.VisibilityStatus,
                DatePosted = model.DatePosted,
                Content = model.Content,
                ImageFileName = fileName,
                UserId = userId
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

        public IQueryable<Posts> GetAllPosts()
        {
            var posts = _context.Post
                .Include(u => u.User)
                .Include(l => l.PostLikes)
                .Include(c => c.PostComments);

            //var posts = _context.Post.Where(v => v.Visibility == Visibility.Everyone);            

            return posts;
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
    }
}
