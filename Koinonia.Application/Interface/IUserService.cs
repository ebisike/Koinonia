using Koinonia.Application.ViewModels;
using Koinonia.Application.ViewModels.Profile;
using Koinonia.Domain.Interface;
using Koinonia.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koinonia.Application.Interface
{
    public interface IUserService : IRepository<KoinoniaUsers>
    {
        Task<KoinoniaUsers> AddNewUser(KoinoniaUserModel model);
        KoinoniaUsers GetUser(Guid UserId);
        ProfileViewModel GetUserProfile(Guid UserId);
        IQueryable<KoinoniaUsers> GetAllUsers();
        bool DeleteUser(Guid UserId);

        Task<KoinoniaUsers> UpdateProfile(EditprofileViewModel model);
    }
}
