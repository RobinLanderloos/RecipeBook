using RecipeBook.Domain.Models.Base;

namespace RecipeBook.Domain.Models
{
    public class IngredientLine : AuditEntity
    {
        public string Ingredient { get; set; }

        public float IngredientAmount { get; set; }

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
        public int UnitOfMeasurementId { get; set; }

        public UnitOfMeasurement UnitOfMeasurement { get; set; }
    }
}
