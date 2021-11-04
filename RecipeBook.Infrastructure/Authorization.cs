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
