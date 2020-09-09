using System;
using System.Collections.Generic;
using System.Text;

namespace Koinonia.Domain.Models
{
    public class Likes
    {
        public Guid Id { get; set; }
        public DateTime DateLiked { get; set; }
        public Guid PostId { get; set; }
        public Posts Posts { get; set; }
        public Guid UserId { get; set; }
        public KoinoniaUsers Users { get; set; }
    }
}
