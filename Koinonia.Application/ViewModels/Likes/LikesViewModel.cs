using Koinonia.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Koinonia.Application.ViewModels.Likes
{
    public class LikesViewModel
    {
        public Guid Id { get; set; }
        public Guid ItemId { get; set; }
        public Guid LikedBy { get; set; }
        public Guid UserId { get; set; }
        public DateTime DateLiked { get; set; }
        public Koinonia.Domain.Models.Posts Posts { get; set; }       
        public KoinoniaUsers User { get; set; }
    }
}
