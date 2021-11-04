using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using RecipeBook.Domain.Models;
using RecipeBook.Infrastructure;
using RecipeBook.Infrastructure.Models.Dtos.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RecipeBook.API.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public UserService(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<AuthenticationDto> GetToken(LoginDto loginDto)
        {
            var authenticationDto = new AuthenticationDto();
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
            {
                authenticationDto.IsAuthenticated = false;
                authenticationDto.Message = $"No account exists with the {nameof(loginDto.Email)} {loginDto.Email}";

                return authenticationDto;
            }

            if (await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                authenticationDto.IsAuthenticated = true;
                JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
                authenticationDto.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                authenticationDto.Email = user.Email;
                var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                authenticationDto.Roles = rolesList.ToList();
                return authenticationDto;
            }

            authenticationDto.IsAuthenticated = false;
            authenticationDto.Message = "Invalid request";
            return authenticationDto;
        }

        private async Task<JwtSecurityToken> CreateJwtToken(Domain.Models.User user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JWT:DurationInMinutes"])),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }

        public async Task<bool> Register(RegisterDto registerDto)
        {
            var user = new User()
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                UserName = registerDto.Email
            };

            if (await _userManager.FindByEmailAsync(user.Email) == null)
            {
                var result = await _userManager.CreateAsync(user, registerDto.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Authorization.DefaultRole.ToString());
                }

                return true;
            }

            return false;
        }
    }
}
