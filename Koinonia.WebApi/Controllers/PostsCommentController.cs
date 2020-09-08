using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Koinonia.Application.Interface;
using Koinonia.Application.ViewModels.Comments;
using Koinonia.Domain.Models;
using Koinonia.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Koinonia.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostsCommentController : ControllerBase
    {
        private readonly IPostsService postsService;
        private readonly ICommentsService commentsService;
        private readonly UserManager<AppUser> userManager;
        private readonly IWebHostEnvironment webHostEnvironment;

        public PostsCommentController(IPostsService postsService,
            ICommentsService commentsService,
            UserManager<AppUser> userManager,
            IWebHostEnvironment webHostEnvironment)
        {
            this.postsService = postsService;
            this.commentsService = commentsService;
            this.userManager = userManager;
            this.webHostEnvironment = webHostEnvironment;
        }

        private async Task<AppUser> GetUser()
        {
            return await userManager.GetUserAsync(User);
        }
        //GET: api/PostComments/{Id}
        [HttpGet("{PostId}")]
        public IActionResult GetPostComments(Guid PostId)
        {
            //get all comments for the particular post
            var comments = commentsService.GetPostComments(PostId);
            return Ok(comments);
        }
        //POST: api/PostComments
        [HttpPost]
        public async Task<IActionResult> Comment(CommentsViewModel model)
        {
            model.userId = Guid.Parse(GetUser().Result.Id);
            await commentsService.CommentOnPost(model);
            return Ok();
        }

        //GET: api/PostComments/{Id}
        [HttpDelete("{CommentId}")]
        public IActionResult DeleteComment(Guid CommentId)
        {
            if(CommentId == null)
            {
                return BadRequest("Cooment Id cannot be null");
            }
            commentsService.Delete(CommentId);
            return NoContent();
        }
    }
}
