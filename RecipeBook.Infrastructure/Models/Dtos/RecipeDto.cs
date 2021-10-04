
namespace RecipeBook.Infrastructure.Models.Dtos
{
    public class RecipeDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Servings { get; set; }

        public ICollection<IngredientLineDto> Ingredients { get; set; }
    }
}
