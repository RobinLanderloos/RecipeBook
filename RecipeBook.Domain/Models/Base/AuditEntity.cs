namespace RecipeBook.Domain.Models.Base
{
    public abstract class AuditEntity : AuditEntity<int>
    {

    }

    public abstract class AuditEntity<T> : BaseEntity<T>
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
