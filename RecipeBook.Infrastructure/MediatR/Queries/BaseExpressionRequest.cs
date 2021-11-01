using MediatR;
using System.Linq.Expressions;

namespace RecipeBook.Infrastructure.MediatR.Queries
{
    public abstract class BaseExpressionRequest<TEntity, TOut> : IRequest<TOut>
    {
        protected BaseExpressionRequest(Expression<Func<TEntity, bool>> expression)
        {
            Expression = expression;
        }

        public Expression<Func<TEntity, bool>> Expression { get; set; }
    }
}
