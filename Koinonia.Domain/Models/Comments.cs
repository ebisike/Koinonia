using System;
using System.Collections.Generic;
using System.Text;

namespace Koinonia.Domain.Models
{
    public class Comments
    {
        public Guid Id { get; set; }
        public string Usercomment { get; set; }
        public Guid PostId { get; set; }
        public Guid NewsId { get; set; }
        public Guid TestimonyId { get; set; }
        public Guid userId { get; set; }
        public DateTime DateCommented { get; set; }
        public Posts post { get; set; }
        public News News { get; set; }
        public Testimonies Testimonies { get; set; }
        public KoinoniaUsers Users { get; set; }
    }
}
