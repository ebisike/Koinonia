using System;
using System.Collections.Generic;
using System.Text;

namespace Koinonia.Domain.Models
{
    public class Followers
    {
        public Guid Id { get; set; }
        public Guid FollowersId { get; set; }
        public Guid UserId { get; set; }
        public KoinoniaUsers Users { get; set; }
    }
}
