using Microsoft.AspNetCore.Identity;

namespace RecipeBook.Domain.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
