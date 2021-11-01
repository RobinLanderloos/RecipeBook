using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RecipeBook.Infrastructure.EntityFramework;
using System.Linq.Expressions;

namespace RecipeBook.Infrastructure.MediatR.Queries.IngredientLine
{
    public class GetIngredientLineByCriteria : BaseExpressionRequest<Domain.Models.IngredientLine, Domain.Models.IngredientLine>
    {
        public GetIngredientLineByCriteria(Expression<Func<Domain.Models.IngredientLine, bool>> expression) : base(expression)
        {
        }
    }

    public class GetIngredientLineByCriteriaHandler : BaseRequestHandler<GetIngredientLineByCriteria, Domain.Models.IngredientLine>
    {
        public GetIngredientLineByCriteriaHandler(RecipeBookContext context, ILogger<GetIngredientLineByCriteriaHandler> logger) : base(context, logger)
        {
        }

        public override async Task<Domain.Models.IngredientLine> Handle(GetIngredientLineByCriteria request, CancellationToken cancellationToken)
        {
            LogHandling(nameof(GetIngredientLineByCriteria));

            if (request.Expression == null)
            {
                throw new ArgumentNullException(nameof(request.Expression));
            }

            try
            {
                _logger.LogInformation($"Getting an {nameof(IngredientLine)} by {request.Expression}");

                var ingredientLine = await _context.IngredientLines.Include(x => x.UnitOfMeasurement).FirstOrDefaultAsync(request.Expression);

                return ingredientLine;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Could not get recipe by expression {request.Expression}: {ex.Message}");

                return null;
            }
        }
    }
}
