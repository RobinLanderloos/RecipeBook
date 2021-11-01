using Microsoft.AspNetCore.Mvc;
using RecipeBook.Domain.Models;
using Microsoft.AspNetCore.Identity;
using MediatR;
using RecipeBook.Infrastructure.Models.Dtos.Authentication;
using RecipeBook.API.Services;

namespace RecipeBook.API.Controllers
{
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> Register(RegisterDto registerDto)
        {
            if (await _userService.Register(registerDto))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("GetToken")]
        public async Task<ActionResult> Login(LoginDto loginDto) 
        {
            var result = await _userService.GetToken(loginDto);

            if(result.Token != null)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
