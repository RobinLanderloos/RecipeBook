using RecipeBook.Infrastructure.Models.Dtos.IngredientLine;

namespace RecipeBook.Infrastructure.Models.Dtos.Recipe
{
    public class RecipeDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Servings { get; set; }

        public ICollection<IngredientLineDto> IngredientLines { get; set; }
    }
}
