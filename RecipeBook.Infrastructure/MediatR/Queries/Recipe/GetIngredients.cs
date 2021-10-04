using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RecipeBook.Domain.Models;
using RecipeBook.Infrastructure.EntityFramework;

namespace RecipeBook.Infrastructure.MediatR.Queries.Recipe
{
    public class GetIngredients : IRequest<IEnumerable<IngredientLine>>
    {
        public int RecipeId { get; set; }

        public GetIngredients(int recipeId)
        {
            RecipeId = recipeId;
        }
    }

    public class GetIngredientsHandler : BaseRequestHandler<GetIngredients, IEnumerable<IngredientLine>>
    {
        public GetIngredientsHandler(RecipeBookContext context, ILogger<GetIngredientsHandler> logger) : base(context, logger)
        {
        }

        public override async Task<IEnumerable<IngredientLine>> Handle(GetIngredients request, CancellationToken cancellationToken)
        {
            LogHandling(nameof(GetIngredients));
            return await _context.IngredientLines.Include(x => x.UnitOfMeasurement).Where(x => x.RecipeId == request.RecipeId).ToListAsync();
        }
    }
}
