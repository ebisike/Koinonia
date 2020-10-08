using Koinonia.Application.Interface;
using Koinonia.Application.ViewModels.Likes;
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
    public class LikesService : Repository<Likes>, ILikesService
    {
        private readonly IRepository<Likes> likesRepo;

        public LikesService(KoinoniaDbContext context, IRepository<Likes> likesRepo) : base(context)
        {
            this.likesRepo = likesRepo;
        }

        public async void DeleteLike(Guid LikeId)
        {
            likesRepo.Delete(LikeId);
            await likesRepo.SaveChangesAsync();
        }

        public IQueryable<Likes> GetPostLikes(Guid PostId)
        {
            var likes = likesRepo.GetAll()
                .Where(x => x.PostId == PostId)
                .Include(u => u.Users);

            return likes;
        }

        public async Task<Likes> LikeAPost(LikesViewModel model)
        {
            var like = new Likes()
            {
                PostId = model.ItemId,
                UserId = model.LikedBy,
                DateLiked = DateTime.Now
            };
            await likesRepo.AddNewAsync(like);
            await likesRepo.SaveChangesAsync();
            return like;
        }
    }
}
