namespace RecipeBook.Infrastructure.Models.Dtos
{
    public class IngredientLineDto : BaseDto
    {
        public string Ingredient { get; set; }

        public float IngredientAmount { get; set; }

        public int RecipeId { get; set; }

        public string UnitOfMeasurement { get; set; }
    }
}
