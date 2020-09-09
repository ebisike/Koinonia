using Koinonia.Application.ViewModels.Likes;
using Koinonia.Domain.Interface;
using Koinonia.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Koinonia.Application.Interface
{
    public interface ILikesService : IRepository<Likes>
    {
        Task<Likes> LikeAPost(LikesViewModel model);
    }
}
