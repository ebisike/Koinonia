using System;
using System.Collections.Generic;
using System.Text;

namespace Koinonia.Domain.Models
{
    public class News
    {
        public News()
        {
            NewsComments = new HashSet<Comments>();
            NewsLikes = new HashSet<Likes>();
        }
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string ImageFileName { get; set; }
        public DateTime DatePosted { get; set; }
        public Visibility visibility { get; set; }

        public Guid UserId { get; set; }
        public KoinoniaUsers User { get; set; }
        public ICollection<Comments> NewsComments { get; set; }
        public ICollection<Likes> NewsLikes { get; set; }
    }
}
