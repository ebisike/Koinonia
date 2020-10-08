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
        Task<Posts> AddNewUserPost(PostsViewModel model, List<string> fileName);
        Task<Posts> GetPost(Guid PostId);
        Task<Posts> UpdatePost(PostsViewModel model, List<string> fileName);
        IQueryable<Posts> GetAllUserStories(int size);
        IQueryable<Posts> GetAllChurchNews(int size);
        IQueryable<Posts> GetAllTestimonies(int size);
        void DeletePost(Guid PostId);
    }
}
