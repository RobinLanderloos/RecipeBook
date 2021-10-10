using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RecipeBook.API.Controllers.Base;
using RecipeBook.API.ResponseHandlers;
using RecipeBook.Domain.Models;
using RecipeBook.Infrastructure.Models.Dtos;

namespace RecipeBook.API.Controllers
{
    [Route("api/[controller]")]
    public class RecipesController : BaseDataAccessController<RecipeDto, RecipeCreateDto, Recipe>
    {
        public RecipesController(IMapper mapper, IMediator mediator, ILogger<RecipesController> logger, IResponseHandler<RecipeDto, RecipeCreateDto, Recipe> responseHandler) : base(mediator, mapper, logger, responseHandler)
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
            return await ResponseHandler.GetEntityByIdAsync(id);
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(RecipeDto), 200)]
        [ProducesResponseType(typeof(Dictionary<string, string>), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<ActionResult<RecipeDto>> Recipes(RecipeCreateDto recipeCreateDto)
        {
            return await ResponseHandler.CreateEntityAsync(recipeCreateDto, ModelState, nameof(Recipes));
        }

        [HttpGet]
        [Route("RecipeByExpression")]
        public async Task<ActionResult<RecipeDto>> RecipeByExpression()
        {
            return await ResponseHandler.GetEntityByCriteria(x => x.Name.Contains("Carbonara"));
        }

        [HttpGet]
        [Route("RecipesByExpression")]
        public async Task<ActionResult<IEnumerable<RecipeDto>>> RecipesByExpression()
        {
            return await ResponseHandler.GetEntitiesByCriteria(x => x.Name.Contains("Testing"));
        }
    }
}
