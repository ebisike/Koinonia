using Koinonia.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Koinonia.Application.ViewModels.Posts
{
    public class PostCommentsViewModel
    {
        public Domain.Models.Posts Post { get; set; }
        public IEnumerable<Domain.Models.Comments> Comments { get; set; }
    }
}
