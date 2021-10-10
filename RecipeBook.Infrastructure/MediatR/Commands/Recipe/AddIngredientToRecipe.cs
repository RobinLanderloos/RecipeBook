using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RecipeBook.Domain.Models;
using RecipeBook.Infrastructure.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.Infrastructure.MediatR.Commands.Recipe
{
    public class AddIngredientToRecipe : IRequest<Domain.Models.Recipe>
    {
        public int RecipeId { get; set; }
        public IngredientLine IngredientLine { get; set; }
    }

    public class AddIngredientToRecipeHandler : BaseRequestHandler<AddIngredientToRecipe, Domain.Models.Recipe>
    {
        public AddIngredientToRecipeHandler(RecipeBookContext context, ILogger<AddIngredientToRecipeHandler> logger) : base(context, logger)
        {
        }

        public override async Task<Domain.Models.Recipe> Handle(AddIngredientToRecipe request, CancellationToken cancellationToken)
        {
            if (request.RecipeId <= 0)
            {
                throw new ArgumentException($"Invalid ID provided");
            }

            if(request.IngredientLine == null)
            {
                throw new ArgumentException($"{nameof(request.IngredientLine)} can not be null");
            }

            var recipe = await _context.Recipes.FirstOrDefaultAsync(x => x.Id == request.RecipeId);

            if (recipe == null)
            {
                return recipe;
            }

            try
            {
                recipe.IngredientLines.Add(request.IngredientLine);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Something went wrong when adding an ingredient to a recipe\n[{nameof(request.RecipeId)} - {request.RecipeId}] [{nameof(request.IngredientLine)} - {request.IngredientLine}");
            }
            
            return recipe;
        }
    }
}
