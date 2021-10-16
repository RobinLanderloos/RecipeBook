using MediatR;
using System.Linq.Expressions;

namespace RecipeBook.Infrastructure.MediatR.Queries
{
    public abstract class BaseExpressionRequest<TEntity, TOut> : IRequest<TOut>
    {
        public Expression<Func<TEntity,bool>> Expression { get; set; }
    }
}
