using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RecipeBook.Infrastructure.EntityFramework;
using System.Linq.Expressions;

namespace RecipeBook.Infrastructure.MediatR.Queries.Recipe
{
    public class GetRecipesByCriteria : BaseExpressionRequest<Domain.Models.Recipe, IEnumerable<Domain.Models.Recipe>>
    {
        public GetRecipesByCriteria(Expression<Func<Domain.Models.Recipe, bool>> expression) : base(expression)
        {
        }
    }

    public class GetRecipesByCriteriaHandler : BaseRequestHandler<GetRecipesByCriteria, IEnumerable<Domain.Models.Recipe>>
    {
        public GetRecipesByCriteriaHandler(RecipeBookContext context, ILogger<GetRecipesByCriteriaHandler> logger) : base(context, logger)
        {
        }

        public override async Task<IEnumerable<Domain.Models.Recipe>> Handle(GetRecipesByCriteria request, CancellationToken cancellationToken)
        {
            LogHandling(nameof(GetRecipesByCriteria));

            if (request.Expression == null)
            {
                throw new ArgumentException(nameof(request.Expression));
            }

            try
            {
                _logger.LogInformation($"Getting all recipes by expression: {request.Expression}");

                var recipes = await _context.Recipes.Include(x => x.IngredientLines).Where(request.Expression).ToListAsync();

                return recipes;
            }
            catch (Exception ex)
            {
                _logger.LogError($"could not get recipes by expression: {request.Expression}: {ex.Message}");
                return null;
            }
        }
    }
}
