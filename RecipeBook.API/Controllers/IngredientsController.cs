using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RecipeBook.API.Controllers.Base;
using RecipeBook.API.ResponseHandlers;
using RecipeBook.Infrastructure.MediatR.Queries.Recipe;
using RecipeBook.Infrastructure.Models.Dtos;

namespace RecipeBook.API.Controllers
{
    [Route("api/{recipeId}/[controller]")]
    public class IngredientsController : BaseDataAccessController<IngredientLineDto, IngredientLineCreateDto>
    {

        public IngredientsController(IMediator mediator, IMapper mapper, ILogger<IngredientsController> logger, IResponseHandler<IngredientLineDto, IngredientLineCreateDto> responseHandler) : base(mediator, mapper, logger, responseHandler)
        {
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IngredientLineDto>>> Ingredients(int recipeId)
        {
            var ingredients = await Mediator.Send(new GetIngredients(recipeId));

            return Ok(Mapper.Map<IEnumerable<IngredientLineDto>>(ingredients));
        }
    }
}
