using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RecipeBook.Infrastructure.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.Infrastructure.MediatR.Queries.IngredientLine
{
    public class GetIngredientLine : IRequest<Domain.Models.IngredientLine>
    {
        public int Id { get; set; }
    }

    public class GetIngredientLineHandler : BaseRequestHandler<GetIngredientLine, Domain.Models.IngredientLine>
    {
        public GetIngredientLineHandler(RecipeBookContext context, ILogger<GetIngredientLine> logger) : base(context, logger)
        {
        }

        public override async Task<Domain.Models.IngredientLine> Handle(GetIngredientLine request, CancellationToken cancellationToken)
        {
            LogHandling(nameof(GetIngredientLine));

            if (request.Id <= 0)
            {
                _logger.LogError($"{nameof(request.Id)} must be greater than 0. Was {request.Id}");
            }

            var ingredientLine = await _context.IngredientLines.Include(x => x.Recipe).Include(x => x.UnitOfMeasurement).FirstOrDefaultAsync(x => x.Id == request.Id);

            if(ingredientLine == null)
            {
                _logger.LogWarning($"Could not find {nameof(Domain.Models.IngredientLine)} with an {nameof(Domain.Models.IngredientLine.Id)} of {request.Id}");
            }

            return null;
        }
    }
}
