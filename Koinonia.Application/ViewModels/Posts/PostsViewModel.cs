using Koinonia.Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Koinonia.Application.ViewModels.Posts
{
    public class PostsViewModel
    {
        public Guid PostId { get; set; }
        public string Content { get; set; }
        public IFormFile Image { get; set; }
        public DateTime DatePosted { get; set; }
        public Visibility VisibilityStatus { get; set; }
        public Category PostCategory { get; set; }
        public Guid userId { get; set; }

        public string ExistingPhotoPath { get; set; }
    }
}
