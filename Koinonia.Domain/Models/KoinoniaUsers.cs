using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Koinonia.Domain.Models
{
    public class KoinoniaUsers
    {
        public KoinoniaUsers()
        {
            UserPosts = new HashSet<Posts>();
            UserLikes = new HashSet<Likes>();
            UserNews = new HashSet<News>();
            UserComments = new HashSet<Comments>();
            UserTestimonies = new HashSet<Testimonies>();
            UserFollowers = new HashSet<Followers>();
        }
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public StateOfOrigin stateOfOrigin { get; set; }
        public Gender Gender { get; set; }

        //public string ApplicationUserId { get; set; }
        //public AppUser ApplicationUser { get; set; }

        public ICollection<Posts> UserPosts { get; set; }
        public ICollection<News> UserNews { get; set; }
        public ICollection<Testimonies> UserTestimonies { get; set; }
        public ICollection<Comments> UserComments { get; set; }
        public ICollection<Likes> UserLikes { get; set; }
        public ICollection<Followers> UserFollowers { get; set; }
    }

    public enum StateOfOrigin
    {
        Abia = 1,
        Adamawa
    }

    public enum Gender
    {
        Male = 1,
        Female
    }

    public enum Visibility
    {
       [Display(Name = "Public")]
       Everyone = 1,
       [Display(Name = "Friends Only")]
       FollowersOnly
    }
}
