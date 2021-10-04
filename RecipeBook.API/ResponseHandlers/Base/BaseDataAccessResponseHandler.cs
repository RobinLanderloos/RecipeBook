using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace RecipeBook.API.ResponseHandlers.Base
{
    public abstract class BaseDataAccessResponseHandler<TReadDto, TCreateDto> : ControllerBase, IResponseHandler<TReadDto, TCreateDto>
    {
        protected BaseDataAccessResponseHandler(IMapper mapper, IMediator mediator, ILogger logger)
        {
            Mapper = mapper;
            Mediator = mediator;
            Logger = logger;
        }

        protected IMapper Mapper { get; }
        protected IMediator Mediator { get; }
        protected ILogger Logger { get; }

        public abstract Task<ActionResult<TReadDto>> CreateRecipe(TCreateDto createDto, ModelStateDictionary modelState, string createdAtName);
        public abstract Task<ActionResult<IEnumerable<TReadDto>>> GetAllRecipes();
        public abstract Task<ActionResult<TReadDto>> GetRecipeById(int id);
    }
}
