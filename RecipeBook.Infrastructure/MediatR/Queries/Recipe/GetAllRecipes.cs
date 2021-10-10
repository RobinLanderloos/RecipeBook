using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RecipeBook.Infrastructure.EntityFramework;

namespace RecipeBook.Infrastructure.MediatR.Queries.Recipe
{
    public class GetAllRecipes : IRequest<IEnumerable<Domain.Models.Recipe>>
    {
    }

    public class GetAllRecipesHandler : BaseRequestHandler<GetAllRecipes, IEnumerable<Domain.Models.Recipe>>
    {
        public GetAllRecipesHandler(RecipeBookContext context, ILogger<GetAllRecipesHandler> logger) : base(context, logger)
        {
        }

        public override async Task<IEnumerable<Domain.Models.Recipe>> Handle(GetAllRecipes request, CancellationToken cancellationToken)
        {
            LogHandling(nameof(GetAllRecipes));
            return _context.Recipes.Include(x => x.IngredientLines).ThenInclude(x => x.UnitOfMeasurement);
        }
    }
}
