using RecipeBook.Domain.Models.Base;

namespace RecipeBook.Domain.Models
{
    public class UnitOfMeasurement : AuditEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
