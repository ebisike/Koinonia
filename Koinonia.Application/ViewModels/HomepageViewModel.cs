using Koinonia.Application.ViewModels.Posts;
using Koinonia.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Koinonia.Application.ViewModels
{
    public class HomepageViewModel
    {
        public IEnumerable<Koinonia.Domain.Models.Posts> AllPosts { get; set; }
        public PostsViewModel NewPost { get; set; }
    }
}
