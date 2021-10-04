namespace RecipeBook.Infrastructure.Models.Dtos
{
    public abstract class BaseDto<T>
    {
        public T Id { get; set; }
    }

    public abstract class BaseDto : BaseDto<int>
    {

    }
}
