using Koinonia.Application.Interface;
using Koinonia.Application.ViewModels;
using Koinonia.Application.ViewModels.Profile;
using Koinonia.Domain.Interface;
using Koinonia.Domain.Models;
using Koinonia.Infra.Data.Context;
using Koinonia.Infra.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koinonia.Application.Services
{
    public class UserService : Repository<KoinoniaUsers>, IUserService
    {
        private readonly IRepository<KoinoniaUsers> usersRepo;
        //private readonly UserManager<IdentityUser> userManager;

        public UserService(KoinoniaDbContext context, IRepository<KoinoniaUsers> usersRepo) : base(context) // UserManager<IdentityUser> userManager
        {
            this.usersRepo = usersRepo;
            //this.userManager = userManager;
        }

        public async Task<KoinoniaUsers> AddNewUser(KoinoniaUserModel model)
        {
            //build new user entity
            KoinoniaUsers koinoniaUsers = new KoinoniaUsers()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Gender = model.Gender,
                stateOfOrigin = model.stateOfOrigin,
            };

            var NewUser = await usersRepo.AddNewAsync(koinoniaUsers);
            usersRepo.SaveChangesAsync().Wait();
            return NewUser;
        }

        public bool DeleteUser(Guid UserId)
        {
            usersRepo.Delete(UserId);
            usersRepo.SaveChanges();
            return true;
        }

        public IQueryable<KoinoniaUsers> GetAllUsers()
        {
            return usersRepo.GetAll();
        }

        public KoinoniaUsers GetUser(Guid UserId)
        {
            return usersRepo.Get(UserId);
        }

        public ProfileViewModel GetUserProfile(Guid UserId)
        {
            ProfileViewModel profileView = new ProfileViewModel();

            ////get Koinonia information [email, phonenumber, username]
            //var appUser = userManager.FindByIdAsync(UserId.ToString()).Result;

            //get koinonia user
            var koinoniaUser = usersRepo.Get(UserId);

            //get user followers
            var followers = _context.Followers
                .Where(x => x.UserId == UserId);

            //get all user stories with reactions
            var UserStories = _context.Post
                .Where(p => p.PostCategory == Category.UserStories && p.UserId == UserId)
                .Include(c => c.PostComments)
                .Include(l => l.PostLikes);

            //get all testimonies
            var testimonies = _context.Post
                .Where(p => p.PostCategory == Category.Testimony && p.UserId == UserId)
                .Include(c => c.PostComments)
                .Include(l => l.PostLikes);

            //get all church news
            var churchNews = _context.Post
                .Where(p => p.PostCategory == Category.News && p.UserId == UserId)
                .Include(c => c.PostComments)
                .Include(l => l.PostLikes);

            //add all values to profileviewmodel

            profileView.ChurchNews = churchNews;
            profileView.Testimonies = testimonies;
            profileView.UserStories = UserStories;
            profileView.Followers = followers;
            profileView.UserInfomation = koinoniaUser;           

            return profileView;
        }

        public async Task<KoinoniaUsers> UpdateProfile(EditprofileViewModel model)
        {
            //get the old profile
            var userprofile = usersRepo.Get(model.Id);

            userprofile.FirstName = model.FirstName;
            userprofile.LastName = model.LastName;
            userprofile.stateOfOrigin = model.StateOfOrigin;
            userprofile.Gender = model.Gender;
            userprofile.PhoneNumber = model.PhoneNumber;

            usersRepo.Update(userprofile);
            await usersRepo.SaveChangesAsync();
            return userprofile;
        }
    }
}
