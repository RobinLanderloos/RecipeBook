using System.ComponentModel.DataAnnotations;

namespace RecipeBook.Infrastructure.Models.Dtos
{
    public class IngredientLineCreateDto
    {
        [Required]
        public string Ingredient { get; set; }

        [Required]
        public float IngredientAmount { get; set; }

        [Required]
        public int UnitOfMeasurementId { get; set; }
    }
}
