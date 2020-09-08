using System;
using System.Collections.Generic;
using System.Text;

namespace Koinonia.Application.ViewModels.Comments
{
    public class CommentsViewModel
    {
        public string UserComment { get; set; }
        public DateTime DateCommented { get; set; }
        public Guid userId { get; set; }
        public Guid PostId { get; set; }
        //public IFormFile CommentImage { get; set; }
    }
}
