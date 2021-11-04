using RecipeBook.Infrastructure.Models.Dtos.Authentication;

namespace RecipeBook.API.Services
{
    public interface IUserService
    {
        public Task<bool> Register(RegisterDto registerDto);
        public Task<AuthenticationDto> GetToken(LoginDto loginDto);
    }
}
