using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RecipeBook.Infrastructure.EntityFramework;
using System.Linq.Expressions;

namespace RecipeBook.Infrastructure.MediatR.Queries.Recipe
{
    public class GetRecipeByCriteria : BaseExpressionRequest<Domain.Models.Recipe, Domain.Models.Recipe>
    {
        public GetRecipeByCriteria(Expression<Func<Domain.Models.Recipe, bool>> expression) : base(expression)
        {
        }
    }

    public class GetRecipeByCriteriaHandler : BaseRequestHandler<GetRecipeByCriteria, Domain.Models.Recipe>
    {
        public GetRecipeByCriteriaHandler(RecipeBookContext context, ILogger<GetRecipeByCriteriaHandler> logger) : base(context, logger)
        {
        }

        public override async Task<Domain.Models.Recipe> Handle(GetRecipeByCriteria request, CancellationToken cancellationToken)
        {
            LogHandling(nameof(GetRecipeByCriteria));

            if (request.Expression == null)
            {
                throw new ArgumentException(nameof(request.Expression));
            }

            try
            {
                _logger.LogInformation($"Getting a recipe by criteria: {request.Expression}");
                var recipe = await _context.Recipes.FirstOrDefaultAsync(request.Expression);

                return recipe;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Could not get recipe by expression {request.Expression}: {ex.Message}");
            }

            return null;
        }
    }
}
