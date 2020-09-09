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
        Task<Posts> AddNewUserPost(PostsViewModel model, string fileName);
        Task<Posts> GetPost(Guid PostId);
        Task<Posts> UpdatePost(PostsViewModel model);
        IQueryable<Posts> GetAllUserStories();
        IQueryable<Posts> GetAllChurchNews();
        IQueryable<Posts> GetAllTestimonies();
        void DeletePost(Guid PostId);
    }
}
