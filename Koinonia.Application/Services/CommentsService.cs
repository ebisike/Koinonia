using Koinonia.Application.Interface;
using Koinonia.Application.ViewModels.Comments;
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
    public class CommentsService : Repository<Comments>, ICommentsService
    {
        private readonly IRepository<Comments> commentRepo;

        public CommentsService(KoinoniaDbContext contex, IRepository<Comments> CommentRepo) : base(contex)
        {
            commentRepo = CommentRepo;
        }

        public async Task<Comments> CommentOnPost(CommentsViewModel model)
        {
            var usercomment = new Comments()
            {
                Usercomment = model.UserComment,
                PostId = model.PostId,
                UserId = model.userId,
                DateCommented = DateTime.Now
            };

            await commentRepo.AddNewAsync(usercomment);
            await commentRepo.SaveChangesAsync();
            return usercomment;
        }

        public async void DeleteComment(Guid commentId)
        {
            commentRepo.Delete(commentId);
            await commentRepo.SaveChangesAsync();
        }

        public IQueryable<Comments> GetPostComments(Guid PostId)
        {
            var comments = commentRepo.GetAll()
                .Where(x => x.PostId == PostId)
                .Include(u => u.Users);

            return comments;
        }
    }
}
