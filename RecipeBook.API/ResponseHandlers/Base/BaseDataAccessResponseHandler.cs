using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq.Expressions;

namespace RecipeBook.API.ResponseHandlers.Base
{
    public abstract class BaseDataAccessResponseHandler<TReadDto, TCreateDto, TEntity, TGetSingleDto> : ControllerBase, IResponseHandler<TReadDto, TCreateDto, TEntity, TGetSingleDto>
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

        public abstract Task<ActionResult> CreateEntityAsync(TCreateDto createDto, ModelStateDictionary modelState, string createdAtName);
        public abstract Task<ActionResult> GetAllEntities();
        public abstract Task<ActionResult> GetEntityByIdAsync(TGetSingleDto getSingleDto);
        public abstract Task<ActionResult> GetEntityByCriteria(Expression<Func<TEntity, bool>> expression);
        public abstract Task<ActionResult> GetEntitiesByCriteria(Expression<Func<TEntity, bool>> expression);
    }
}
