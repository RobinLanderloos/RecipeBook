using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeBook.API.Controllers.Base;
using RecipeBook.API.ResponseHandlers;
using RecipeBook.Domain.Models;
using RecipeBook.Infrastructure.Models.Dtos.Recipe;

namespace RecipeBook.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class RecipesController : BaseDataAccessController<RecipeDto, RecipeCreateDto, Recipe, GetSingleRecipeDto>
    {
        public RecipesController(IMapper mapper, ILogger<RecipesController> logger, IResponseHandler<RecipeDto, RecipeCreateDto, Recipe, GetSingleRecipeDto> responseHandler) : base(mapper, logger, responseHandler)
        {
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(IEnumerable<RecipeDto>), 200)]
        public async Task<ActionResult<IEnumerable<RecipeDto>>> Recipes()
        {
            return await ResponseHandler.GetAllEntities();
        }

        [HttpGet]
        [Route("{id}", Name = "GetRecipeById")]
        [ProducesResponseType(typeof(RecipeDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<RecipeDto>> Recipes(int id)
        {
            return await ResponseHandler.GetEntityByIdAsync(new GetSingleRecipeDto() { Id = id });
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(RecipeDto), 201)]
        [ProducesResponseType(typeof(Dictionary<string, string>), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<ActionResult> Recipes(RecipeCreateDto recipeCreateDto)
        {
            return await ResponseHandler.CreateEntityAsync(recipeCreateDto, ModelState, nameof(Recipes));
        }
    }
}
