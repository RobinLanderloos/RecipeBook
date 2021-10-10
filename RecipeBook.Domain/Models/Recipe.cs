using RecipeBook.Domain.Models.Base;

namespace RecipeBook.Domain.Models
{
    public class Recipe : AuditEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Servings { get; set; }

        public ICollection<IngredientLine> IngredientLines { get; set; }

        public override string ToString()
        {
            return $"{Id} - {Name}\n{Description}\nAmount of ingredients: {IngredientLines?.Count}";
        }
    }
}
