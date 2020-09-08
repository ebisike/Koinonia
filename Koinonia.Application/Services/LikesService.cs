using Koinonia.Application.Interface;
using Koinonia.Application.ViewModels.Likes;
using Koinonia.Domain.Interface;
using Koinonia.Domain.Models;
using Koinonia.Infra.Data.Context;
using Koinonia.Infra.Data.Repositories;
using System;
using System.Collections.Generic;
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

        public Task<Likes> LikeANews(LikesViewModel model)
        {
            throw new NotImplementedException();
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

        public Task<Likes> LikeATestimony(LikesViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
