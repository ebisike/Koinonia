using Koinonia.Application.Interface;
using Koinonia.Domain.Interface;
using Koinonia.Domain.Models;
using Koinonia.Infra.Data.Context;
using Koinonia.Infra.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koinonia.Application.Services
{
    public class FollowService : Repository<Followers>, IFollowService
    {
        private readonly IRepository<Followers> followership;

        public FollowService(KoinoniaDbContext context, IRepository<Followers> followership) : base(context)
        {
            this.followership = followership;
        }

        public async Task<Followers> FollowUser(Guid FollowerId, Guid FollowingId)
        {
            //buiild the entity
            Followers followers = new Followers()
            {
                Id = Guid.NewGuid(),
                FollowersId = FollowerId,
                UserId = FollowingId,
            };

            //add to db
            await followership.AddNewAsync(followers);
            await followership.SaveChangesAsync();
            return followers;
        }

        public async Task<bool> UnFollowUser(Guid FollowerId, Guid UserId)
        {
            var entity = followership.GetAll().Where(x => x.FollowersId == FollowerId && x.UserId == UserId).FirstOrDefault();
            if(entity != null)
            {
                followership.Delete(entity.Id);
                if(await followership.SaveChangesAsync())
                {
                    return true;
                }
            }
            return false;
        }
    }
}
