using System;
using System.Collections.Generic;
using System.Text;

namespace Koinonia.Domain.Models
{
    public class Posts
    {
        public Posts()
        {
            PostComments = new HashSet<Comments>();
            PostLikes = new HashSet<Likes>();
        }
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime DatePosted { get; set; }
        public List<MediaFiles> MediaFiles { get; set; }
        public Visibility Visibility { get; set; }
        public Category PostCategory { get; set; }

        public Guid UserId { get; set; }
        public KoinoniaUsers User { get; set; }
        public ICollection<Comments> PostComments { get; set; }
        public ICollection<Likes> PostLikes { get; set; }
    }
}
