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
using Koinonia.WebApi.Migrations;
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
    public class UserStoriesController : ControllerBase
    {
        private readonly IPostsService postService;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly UserManager<AppUser> userManager;
        private readonly KoinoniaDbContext context;
        public string Folder { get; set; }

        public UserStoriesController(IPostsService postService,
            IWebHostEnvironment webHostEnvironment,
            UserManager<AppUser> userManager,
            KoinoniaDbContext context)
        {
            this.postService = postService;
            this.webHostEnvironment = webHostEnvironment;
            this.userManager = userManager;
            this.context = context;
            
            //build the folder
            this.Folder = Path.Combine(webHostEnvironment.WebRootPath, "Posts/UserStories");
        }

        private async Task<AppUser> GetUser()
        {
            return await userManager.GetUserAsync(User);
        }
        
        /// <summary>
        /// Returns an Iquerable result of user posts under the UserStories category
        /// </summary>
        /// <returns></returns>
        /// 

        //GET: api/UserStories
        [AllowAnonymous]        
        [HttpGet]
        public IActionResult Index()
        {
            var stories = postService.GetAllUserStories();
            //var stories = postService.GetAllUserStories().SelectMany(d => d.PostComments, (User, Comment) => new
            //{
            //    userName = User.User.FirstName + " " + User.User.LastName,
            //    Comment = Comment.Usercomment,
            //    dateCommented = Comment.DateCommented.ToLongDateString(),
            //    PostId = Comment.PostId
            //});
            return Ok(stories);
        }

        //POST: api/Posts
        [HttpPost]
        public async Task<ActionResult<PostsViewModel>> NewPost(PostsViewModel model)
        {
            List<string> fileName = null;
            if (model != null)
            {
                //handle the upload if the user added an image to the post
                if(model.Image != null && model.Image.Count() > 0)
                {
                    foreach (var media in model.Image)
                    {
                        //verify file extension
                        var ext = Path.GetExtension(media.FileName).ToLower();
                        if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".mp4" || ext == ".3gp")
                        {
                            fileName.Add(UploadFile.Upload(media, Folder));
                        }
                        else
                        {
                            return BadRequest("file format not supported");
                        }
                    }
                }
                
                model.DatePosted = DateTime.Now; //set the timestamp
                model.userId = Guid.Parse(GetUser().Result.Id); //set the user id
                model.PostCategory = Category.UserStories; //set the post category as UserStories

                var addedPost = await postService.AddNewUserPost(model, fileName);

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
        [HttpDelete("{Id}")]
        public IActionResult DeletePost(Guid Id)
        {
            if(Id == null)
            {
                return BadRequest("Post ID cannot be null");
            }
            var post = postService.Get(Id);
            foreach (var item in post.MediaFiles)
            {
                string path = Path.Combine(Folder, item.fileName);
                //delete the post file if any
                UploadFile.DeleteFile(path);
            }
            postService.DeletePost(Id);
            return Ok(new { message = "Post Deleted"});
        }

        //GET: api/Posts
        [HttpGet]
        [Route("UpdateUserStory")]
        public IActionResult UpdateUserStory(Guid PostId)
        {
            var post = postService.Get(PostId);
            PostsViewModel postsView = new PostsViewModel()
            {
                PostId = post.Id,
                Content = post.Content,
                userId = post.UserId,
                PostCategory = post.PostCategory,
                DatePosted = post.DatePosted,
                VisibilityStatus = post.Visibility,
            };
            return Ok(postsView);
        }
        //POST: api/Posts
        [HttpPut]
        [Route("UpdateUserStory")]
        public async Task<IActionResult> UpdateUserStory(PostsViewModel model)
        {
            if(model != null)
            {
                if(model.Image != null && model.Image.Count() > 0 )
                {
                    foreach (var item in model.Image)
                    {
                        model.ExistingPhotoPath.Add(UploadFile.Upload(item, Folder));
                    }
                }
                await postService.UpdatePost(model, model.ExistingPhotoPath);
                return Ok();
            }

            return BadRequest(new { message = "Model cannot be null" });
        }
    }
}
