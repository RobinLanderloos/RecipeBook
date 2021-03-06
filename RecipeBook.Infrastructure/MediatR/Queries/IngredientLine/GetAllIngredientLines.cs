using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RecipeBook.Infrastructure.EntityFramework;

namespace RecipeBook.Infrastructure.MediatR.Queries.IngredientLine
{
    public class GetAllIngredientLines : IRequest<IEnumerable<Domain.Models.IngredientLine>>
    {

    }

    public class GetAllIngredientLinesHandler : BaseRequestHandler<GetAllIngredientLines, IEnumerable<Domain.Models.IngredientLine>>
    {
        public GetAllIngredientLinesHandler(RecipeBookContext context, ILogger<GetAllIngredientLinesHandler> logger) : base(context, logger)
        {
        }

        public override async Task<IEnumerable<Domain.Models.IngredientLine>> Handle(GetAllIngredientLines request, CancellationToken cancellationToken)
        {
            return await _context.IngredientLines.Include(x => x.UnitOfMeasurement).ToListAsync();
        }
    }
}
