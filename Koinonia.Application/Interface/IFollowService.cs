using Koinonia.Domain.Interface;
using Koinonia.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Koinonia.Application.Interface
{
    public interface IFollowService : IRepository<Followers>
    {
        Task<Followers> FollowUser(Guid FollowerId, Guid UserId);
        Task<bool> UnFollowUser(Guid FollowerId, Guid UserId);
    }
}
