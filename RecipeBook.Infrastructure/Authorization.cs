using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.Infrastructure
{
    public class Authorization
    {
        public enum Roles
        {
            Administrator,
            User
        }

        public const Roles DefaultRole = Roles.User;
    }
}
