using Koinonia.Application.ViewModels.Comments;
using Koinonia.Application.ViewModels.Posts;
using Koinonia.Domain.Interface;
using Koinonia.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koinonia.Application.Interface
{
    public interface IPostsService : IRepository<Posts>
    {
        Task<Posts> AddNewUserPost(PostsViewModel model, string fileName, Guid userId);
        Task<Posts> GetPost(Guid PostId);
        IQueryable<Posts> GetAllPosts();
        void DeletePost(Guid PostId);
    }
}
