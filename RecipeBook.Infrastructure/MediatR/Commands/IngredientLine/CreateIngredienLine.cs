using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RecipeBook.Infrastructure.EntityFramework;

namespace RecipeBook.Infrastructure.MediatR.Commands.IngredientLine
{
    public class CreateIngredienLine : IRequest<Domain.Models.IngredientLine>
    {
        public Domain.Models.IngredientLine IngredientLine { get; set; }
    }

    public class CreateIngredientLineHandler : BaseRequestHandler<CreateIngredienLine, Domain.Models.IngredientLine>
    {
        public CreateIngredientLineHandler(RecipeBookContext context, ILogger<CreateIngredientLineHandler> logger) : base(context, logger)
        {
        }

        public override async Task<Domain.Models.IngredientLine> Handle(CreateIngredienLine request, CancellationToken cancellationToken)
        {
            if (request.IngredientLine.RecipeId <= 0)
            {
                throw new ArgumentException($"Invalid ID provided");
            }

            if (request.IngredientLine == null)
            {
                throw new ArgumentException($"{nameof(request.IngredientLine)} can not be null");
            }

            if (!_context.Recipes.Any(x => x.Id == request.IngredientLine.RecipeId))
            {
                return null;
            }

            try
            {
                _context.IngredientLines.Add(request.IngredientLine);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong when creating a new \n[{nameof(request.IngredientLine)}]");
            }



            return await _context.IngredientLines.Include(x => x.UnitOfMeasurement).Include(x => x.Recipe).FirstOrDefaultAsync(x => x.Id == request.IngredientLine.Id);
        }
    }
}
