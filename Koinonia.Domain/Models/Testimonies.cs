using System;
using System.Collections.Generic;
using System.Text;

namespace Koinonia.Domain.Models
{
    public class Testimonies
    {
        public Testimonies()
        {
            TestimonyComments = new HashSet<Comments>();
            TestimonyLikes = new HashSet<Likes>();
        }
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string ImageFileName { get; set; }
        public DateTime DatePosted { get; set; }
        public Visibility visibility { get; set; }

        public Guid UserId { get; set; }
        public KoinoniaUsers User { get; set; }
        public ICollection<Comments> TestimonyComments { get; set; }
        public ICollection<Likes> TestimonyLikes { get; set; }
    }
}
