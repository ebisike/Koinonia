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
        public string Content { get; set; }
        public IFormFile Image { get; set; }
        public DateTime DatePosted { get; set; }
        public Visibility VisibilityStatus { get; set; }
    }
}
