using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Koinonia.Application.Interface;
using Koinonia.Application.Services;
using Koinonia.Application.ViewModels.Posts;
using Koinonia.Domain.Models;
using Koinonia.Infra.Data.Context;
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
    [AllowAnonymous]
    public class PostsController : ControllerBase
    {
        private readonly IPostsService postService;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly UserManager<AppUser> userManager;
        private readonly KoinoniaDbContext context;

        public AppUser LogedInUser { get; set; }
        public string Folder { get; set; }

        public PostsController(IPostsService postService,
            IWebHostEnvironment webHostEnvironment,
            UserManager<AppUser> userManager,
            KoinoniaDbContext context)
        {
            this.postService = postService;
            this.webHostEnvironment = webHostEnvironment;
            this.userManager = userManager;
            this.context = context;
            
            //build the folder
            this.Folder = Path.Combine(webHostEnvironment.WebRootPath, "Posts");
        }

        private async Task<AppUser> GetUser()
        {
            return await userManager.GetUserAsync(User);
        }
        /// <summary>
        /// This endpoint is the homepage of the application.
        /// The user can view all the posts in the application
        /// </summary>
        /// <returns></returns>
        //GET: api/Posts
        [AllowAnonymous]        
        [HttpGet]
        public IActionResult Index()
        {
            var allPosts = postService.GetAllPosts();
            return Ok(allPosts);
        }

        //POST: api/Posts
        [HttpPost]
        public async Task<ActionResult<PostsViewModel>> NewPost(PostsViewModel model)
        {
            string fileName = null;
            if (model != null)
            {
                //handle the upload if the user added an image to the post
                if(model.Image != null)
                {
                    //verify file extension
                    var ext = Path.GetExtension(model.Image.FileName).ToLower();
                    if(ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".mp4")
                    {
                        fileName = UploadFile.Upload(model.Image, Folder);
                    }
                    else
                    {
                        return BadRequest("file format not supported");
                    }
                    
                }
                LogedInUser = await GetUser();
                model.DatePosted = DateTime.Now;
                var addedPost = await postService.AddNewUserPost(model, fileName, Guid.Parse(LogedInUser.Id));
                return CreatedAtRoute(nameof(GetPost), new { addedPost.Id }, addedPost);
            }
            return BadRequest();
        }

        //GET: api/Posts/{Id}
        [HttpGet("{Id}", Name = "GetPost")]
        public async Task<IActionResult> GetPost(Guid Id)
        {
            if(Id != null)
            {
                var post = await postService.GetPost(Id);
                if(post != null)
                {
                    return Ok(post);
                }
                return NoContent();
            }
            return BadRequest();
        }

        //GET api/Posts/{id}
        [HttpDelete]
        public IActionResult DeletePost(Guid Id)
        {
            if(Id == null)
            {
                return BadRequest("Post ID cannot be null");
            }
            postService.DeletePost(Id);
            return Ok();
        }
    }
}
