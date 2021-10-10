using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RecipeBook.Infrastructure.EntityFramework;

namespace RecipeBook.Infrastructure.MediatR.Commands.Recipe
{
    public class CreateRecipe : IRequest<Domain.Models.Recipe>
    {
        public readonly Domain.Models.Recipe Recipe;

        public CreateRecipe(Domain.Models.Recipe recipe)
        {
            Recipe = recipe;
        }
    }

    public class CreateRecipeHandler : BaseRequestHandler<CreateRecipe, Domain.Models.Recipe>
    {
        public CreateRecipeHandler(RecipeBookContext context, ILogger<CreateRecipeHandler> logger) : base(context, logger)
        {

        }

        public override async Task<Domain.Models.Recipe> Handle(CreateRecipe request, CancellationToken cancellationToken)
        {
            LogHandling(nameof(CreateRecipe));

            if (request.Recipe == null)
            {
                throw new ArgumentException(nameof(request.Recipe));
            }

            try
            {
                _logger.LogInformation($"Adding new {nameof(request.Recipe)}\n{request.Recipe}");

                await _context.AddAsync(request.Recipe, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Could not create {nameof(request.Recipe)}: {ex.Message}");
                return request.Recipe;
            }

            _logger.LogInformation($"Successfully added new {nameof(request.Recipe)}");
            var recipe = await _context.Recipes
                .Include(x => x.IngredientLines)
                .ThenInclude(x => x.UnitOfMeasurement)
                .FirstOrDefaultAsync(x => x.Id == request.Recipe.Id);
            return request.Recipe;
        }
    }
}
