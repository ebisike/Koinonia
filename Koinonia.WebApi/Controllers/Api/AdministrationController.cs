using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Koinonia.Application.Interface;
using Koinonia.Application.ViewModels.Administration;
using Koinonia.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Koinonia.WebApi.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministrationController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserService _userService;
        private readonly IPostsService _postService;

        public AdministrationController(UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IUserService userService,
            IPostsService postsService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userService = userService;
            _postService = postsService;
        }

        [HttpGet]
        [Route("AllUsers")]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            return Ok(new { users, count = users.Count() });
        }

        [HttpGet]
        [Route("Stories")]
        public IActionResult GetStories()
        {
            var UserStories = _postService.GetAllUserStories();
            return Ok(new { UserStories, count = UserStories.Count() });
        }

        [HttpGet]
        [Route("ChurchNews")]
        public IActionResult GetChurchNews()
        {
            var ChurchNews = _postService.GetAllChurchNews();
            return Ok(new { ChurchNews, count = ChurchNews.Count() });
        }

        [HttpGet]
        [Route("Testimonies")]
        public IActionResult GetTestimonies()
        {
            var Testimonies = _postService.GetAllTestimonies();
            return Ok(new { Testimonies, count = Testimonies.Count() });
        }

        [HttpGet("{postId}")]
        [Route("Post")]
        public async Task<IActionResult> Story(Guid postId)
        {
            var post = await _postService.GetPost(postId);
            if (post != null)
            {
                return Ok(post);
            }
            return BadRequest(new { message = "Sorry post not found" });
        }

        [HttpDelete("{postId}")]
        [Route("DeletePost")]
        public IActionResult DeletePost(Guid postId)
        {
            _postService.DeletePost(postId);
            return Ok();
        }

        [HttpGet]
        [Route("ListAppUsers")]
        public IActionResult ListAppUsers()
        {
            var AllUsers = _userManager.Users.ToList();
            return Ok(new { AllUsers, count = AllUsers.Count() });
        }

        [HttpGet]
        [Route("AllRoles")]
        public IActionResult AllRoles()
        {
            var AllRoles = _roleManager.Roles.ToList();
            if (AllRoles != null)
                return Ok(new { AllRoles, count = AllRoles.Count() });

            return NotFound(new { message = "No Roles Registered" });
        }

        [HttpGet("{roleId}")]
        [Route("UsersInRole")]
        public async Task<IActionResult> ListOfUsersInRole(string roleId)
        {
            //get the role by Id
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
                return NotFound();

            //create a new list of role with users
            List<UsersInRole> usersInRoles = new List<UsersInRole>();
            UsersInRole inRole = new UsersInRole();

            foreach (var user in _userManager.Users.ToList())
            {
                if(await _userManager.IsInRoleAsync(user, role.Name))
                {
                    inRole.AppUserId = user.Id;
                    inRole.Username = user.UserName;

                    //add the user to the list
                    usersInRoles.Add(inRole);
                }
            }

            var model = new RoleViewModel();
            model.Id = role.Id;
            model.RoleName = role.Name;
            model.RoleUsers = usersInRoles;

            return Ok(model);
        }

        [HttpPost]
        [Route("createRole")]
        public async Task<IActionResult> CreateRole(RoleViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.RoleName))
                return BadRequest(new { message = "Role Name cannot be empty" });

            try
            {
                if (!await _roleManager.RoleExistsAsync(model.RoleName))
                {
                    IdentityRole identityRole = new IdentityRole()
                    {
                        Name = model.RoleName
                    };
                    IdentityResult result = await _roleManager.CreateAsync(identityRole);
                    if (result.Succeeded)
                    {
                        return CreatedAtAction("CreateRole", new { Id = identityRole.Id, Name = identityRole.Name });
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("EditRole")]
        public async Task<IActionResult> EditRole(RoleViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);
            if (role == null)
                return NotFound();

            role.Name = model.RoleName;
            var updateRole = await _roleManager.UpdateAsync(role);
            if (updateRole.Succeeded)
                return Ok(new { message = "Role updated" });
            return BadRequest(new { message = "Failed to update role" });
        }

        [HttpDelete]
        [Route("DeleteRoles")]
        public async Task<ActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            var result = await _roleManager.DeleteAsync(role);

            return Ok(result);
        }
        
        [HttpPut]
        [Route("AddUserToRole")]
        public async Task<IActionResult> AddUserToRole(UsersRoleViewModel model)
        {
            //first find the role
            var role = await _roleManager.FindByNameAsync(model.RoleName);
            if (role == null)
                return NotFound(new { message = "Role Not Found"});

            //find the user by Id
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
                return NotFound(new { message = "User not Found" });

            //add the user to the role
            var result = await _userManager.AddToRoleAsync(user, role.Name);
            if (result.Succeeded)
                return Ok(new { message = "User added to role" });

            return BadRequest(new { message = "Failed to add user" });
        }

        [HttpDelete]
        [Route("DeleteUserFromRole")]
        public async Task<IActionResult> DeleteUserFromRole(UsersRoleViewModel model)
        {
            //find the role
            var role = await _roleManager.FindByNameAsync(model.RoleName);
            if (role == null)
                return NotFound(new { message = "Role Not Found" });

            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
                return NotFound(new { message = "user not found" });

            var result = await _userManager.RemoveFromRoleAsync(user, role.Name);
            if (result.Succeeded)
                return Ok(new { message = "Removed successfully" });

            return BadRequest(new { message = "Failed to Remove User" });
        }
    }
}
