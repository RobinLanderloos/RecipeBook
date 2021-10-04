namespace RecipeBook.Domain.Models.Base
{
    public abstract class BaseEntity : BaseEntity<int>
    {

    }

    public abstract class BaseEntity<T>
    {
        public T Id { get; set; }
    }
}
