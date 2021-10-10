using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RecipeBook.Infrastructure.EntityFramework;

namespace RecipeBook.Infrastructure.MediatR.Queries.Recipe
{
    public class GetRecipe : IRequest<Domain.Models.Recipe>
    {
        public int RecipeId { get; set; }

        public GetRecipe(int recipeId)
        {
            RecipeId = recipeId;
        }
    }

    public class GetRecipeHandler : BaseRequestHandler<GetRecipe, Domain.Models.Recipe>
    {
        public GetRecipeHandler(RecipeBookContext context, ILogger<GetRecipeHandler> logger) : base(context, logger)
        {
        }

        public override async Task<Domain.Models.Recipe> Handle(GetRecipe request, CancellationToken cancellationToken)
        {
            LogHandling(nameof(GetRecipe));

            if (request.RecipeId <= 0)
            {
                _logger.LogError($"{nameof(request.RecipeId)} must be greater than 0. Was {request.RecipeId}");
                return null;
            }

            var recipe = await _context.Recipes.Include(x => x.IngredientLines).ThenInclude(x => x.UnitOfMeasurement).FirstOrDefaultAsync(x => x.Id == request.RecipeId);

            if (recipe == null)
            {
                _logger.LogWarning($"Could not find {nameof(Domain.Models.Recipe)} with an {nameof(Domain.Models.Recipe.Id)} of [{request.RecipeId}]");
            }

            return recipe;
        }
    }
}
