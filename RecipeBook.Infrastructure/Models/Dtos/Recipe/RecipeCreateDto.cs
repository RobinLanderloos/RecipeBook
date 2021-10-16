using RecipeBook.Infrastructure.Models.Dtos.IngredientLine;
using System.ComponentModel.DataAnnotations;

namespace RecipeBook.Infrastructure.Models.Dtos.Recipe
{
    public class RecipeCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public float Servings { get; set; }
        [Required]
        public ICollection<IngredientLineCreateDto> Ingredients { get; set; }
    }
}
