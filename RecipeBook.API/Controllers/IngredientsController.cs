using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RecipeBook.API.Controllers.Base;
using RecipeBook.API.ResponseHandlers;
using RecipeBook.Domain.Models;
using RecipeBook.Infrastructure.Models.Dtos.IngredientLine;

namespace RecipeBook.API.Controllers
{
    [Route("api/{recipeId}/[controller]")]
    public class IngredientsController : BaseDataAccessController<IngredientLineDto, IngredientLineCreateDto, IngredientLine, GetSingleIngredientDto>
    {

        public IngredientsController(IMapper mapper, ILogger<IngredientsController> logger, IResponseHandler<IngredientLineDto, IngredientLineCreateDto, IngredientLine, GetSingleIngredientDto> responseHandler) : base(mapper, logger, responseHandler)
        {
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(IEnumerable<IngredientLineDto>), 200)]
        public async Task<ActionResult<IEnumerable<IngredientLineDto>>> Ingredients(int recipeId)
        {
            return await ResponseHandler.GetAllEntities();
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(IngredientLineDto), 200)]
        public async Task<ActionResult<IngredientLineDto>> Ingredients(IngredientLineCreateDto dto)
        {
            return await ResponseHandler.CreateEntityAsync(dto, ModelState, nameof(Ingredients));
        }
    }
}
