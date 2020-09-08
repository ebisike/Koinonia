using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Koinonia.Application.Interface;
using Koinonia.Application.ViewModels.Likes;
using Koinonia.Domain.Models;
using Koinonia.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Koinonia.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostsLikesController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly ILikesService likesService;       

        private AppUser _user;

        public PostsLikesController(UserManager<AppUser> userManager,
            ILikesService likesService)
        {
            this.userManager = userManager;
            this.likesService = likesService;

            _user = GetUser().Result;
        }

        private async Task<AppUser> GetUser()
        {
            return await userManager.GetUserAsync(User);
        }

        [HttpPost]
        public IActionResult LikePost(LikesViewModel model)
        {
            model.LikedBy = Guid.Parse(GetUser().Result.Id);
            likesService.LikeAPost(model);
            return Ok();
        }
    }
}
