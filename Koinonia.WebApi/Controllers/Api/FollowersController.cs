using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Koinonia.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Koinonia.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FollowersController : ControllerBase
    {
        private readonly IFollowService followService;

        public FollowersController(IFollowService followService)
        {
            this.followService = followService;
        }

        /// <summary>
        /// This endpoint is used to follow a koinonia User
        /// </summary>
        /// <param name="FollowerId"></param>
        /// <param name="UserId"></param>
        /// <returns>Status 200</returns>
        //POST: api/Follow
        [HttpPost]
        [Route("Follow")]
        public async Task<IActionResult> Follow(Guid FollowerId, Guid UserId)
        {
            if(FollowerId != null && UserId != null)
            {
                var result = await followService.FollowUser(FollowerId, UserId);
                if(result != null)
                {
                    return Ok(new { message = "You're now following the user" });
                }
            }

            return BadRequest(new { message = "Values cannot be null" });
        }

        //POST: api/Follow
        [HttpPost]
        [Route("Unfollow")]
        public async Task<IActionResult> Unfollow(Guid FollowerId, Guid FollowingId)
        {
            if(FollowerId != null && FollowingId != null)
            {
                var result = await followService.UnFollowUser(FollowerId, FollowingId);
                if (result)
                {
                    return Ok(new { message = "Unfollow successful" });
                }
            }
            return BadRequest(new { message = "Values cannot be null" });
        }
    }
}
