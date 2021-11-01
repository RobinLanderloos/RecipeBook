using Microsoft.AspNetCore.Mvc;
using RecipeBook.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.API.Controllers
{
    public class UserController : ControllerBase
    {
        private UserManager<User> _userManager = null;
        private SignInManager<User> _signInManager = null;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult> Create(string username, string email, string password)
        {
            var result = await _userManager.CreateAsync(
                new User()
                {
                    UserName = username,
                    Email = email,
                }, password);

            if (result.Succeeded)
            {
                return Ok(username);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login(string username, string password) 
        {
            var user = await _userManager.FindByNameAsync(username);
            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (result.Succeeded)
            {
                return Ok("Logged in");
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
