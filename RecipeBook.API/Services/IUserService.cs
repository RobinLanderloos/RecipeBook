using RecipeBook.Infrastructure.Models.Dtos.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.API.Services
{
    public interface IUserService
    {
        public Task<bool> Register(RegisterDto registerDto);
        public Task<AuthenticationDto> GetToken(LoginDto loginDto);
    }
}
