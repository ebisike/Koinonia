using Koinonia.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Koinonia.Application.ViewModels.Profile
{
    public class ProfileViewModel
    {
        public IQueryable<Koinonia.Domain.Models.Posts> UserStories { get; set; }
        public IQueryable<Koinonia.Domain.Models.Posts> ChurchNews { get; set; }
        public IQueryable<Koinonia.Domain.Models.Posts> Testimonies { get; set; }
        public IQueryable<Followers> Followers { get; set; }
        public KoinoniaUsers UserInfomation { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Username { get; set; }
    }
}
