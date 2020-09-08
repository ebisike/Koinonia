using System;
using System.Collections.Generic;
using System.Text;

namespace Koinonia.Domain.Models
{
    public class Likes
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; }
        public Guid NewsId { get; set; }
        public Guid TestimonyId { get; set; }
        public Guid UserId { get; set; }
        public Posts posts { get; set; }
        public News News { get; set; }
        public Testimonies Testimony { get; set; }
        public KoinoniaUsers Users { get; set; }
        public DateTime DateLiked { get; set; }
    }
}
