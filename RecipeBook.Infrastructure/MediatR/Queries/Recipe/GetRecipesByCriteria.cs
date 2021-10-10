using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RecipeBook.Infrastructure.EntityFramework;
using RecipeBook.Infrastructure.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.Infrastructure.MediatR.Queries.Recipe
{
    public class GetRecipesByCriteria : IRequest<IEnumerable<Domain.Models.Recipe>>
    {
        public Expression<Func<Domain.Models.Recipe, bool>> Expression { get; set; }
    }

    public class GetRecipesByCriteriaHandler : BaseRequestHandler<GetRecipesByCriteria, IEnumerable<Domain.Models.Recipe>>
    {
        public GetRecipesByCriteriaHandler(RecipeBookContext context, ILogger<GetRecipesByCriteriaHandler> logger) : base(context, logger)
        {
        }

        public override async Task<IEnumerable<Domain.Models.Recipe>> Handle(GetRecipesByCriteria request, CancellationToken cancellationToken)
        {
            LogHandling(nameof(GetRecipesByCriteria));

            if(request.Expression == null)
            {
                throw new ArgumentException(nameof(request.Expression));
            }

            try
            {
                _logger.LogInformation($"Getting all recipes by expression: {request.Expression}");

                var recipes = await _context.Recipes.Where(request.Expression).ToListAsync();

                return recipes;
            }
            catch(Exception ex)
            {
                _logger.LogError($"could not get recipes by expression: {request.Expression}: {ex.Message}");
                return null;
            }
        }
    }
}
