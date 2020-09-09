using System;
using System.Collections.Generic;
using System.Text;

namespace Koinonia.Domain.Models
{
    public class Comments
    {
        public Guid Id { get; set; }
        public string Usercomment { get; set; }
        public DateTime DateCommented { get; set; }
        public Guid PostId { get; set; }
        public Posts Post { get; set; }
        public Guid UserId { get; set; }
        public KoinoniaUsers Users { get; set; }
    }
}
