using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RecipeBook.Infrastructure.EntityFramework;
using System.Linq.Expressions;

namespace RecipeBook.Infrastructure.MediatR.Queries.IngredientLine
{
    public class GetIngredientLinesByCriteria : BaseExpressionRequest<Domain.Models.IngredientLine, IEnumerable<Domain.Models.IngredientLine>>
    {
        public GetIngredientLinesByCriteria(Expression<Func<Domain.Models.IngredientLine, bool>> expression) : base(expression)
        {
        }
    }

    public class GetIngredientLinesByCriteriaHandler : BaseRequestHandler<GetIngredientLinesByCriteria, IEnumerable<Domain.Models.IngredientLine>>
    {
        public GetIngredientLinesByCriteriaHandler(RecipeBookContext context, ILogger<GetIngredientLinesByCriteriaHandler> logger) : base(context, logger)
        {
        }

        public override async Task<IEnumerable<Domain.Models.IngredientLine>> Handle(GetIngredientLinesByCriteria request, CancellationToken cancellationToken)
        {
            LogHandling(nameof(GetIngredientLinesByCriteria));

            if (request.Expression == null)
            {
                throw new ArgumentException(nameof(request.Expression));
            }

            try
            {
                _logger.LogInformation($"Getting all {nameof(IngredientLine)} by expression {request.Expression}");

                var ingredientLines = await _context.IngredientLines.Include(x => x.UnitOfMeasurement).Where(request.Expression).ToListAsync(cancellationToken);

                return ingredientLines;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Could not get {nameof(IngredientLine)} by expression: {request.Expression}: {ex.Message}");
                return null;
            }
        }
    }
}
