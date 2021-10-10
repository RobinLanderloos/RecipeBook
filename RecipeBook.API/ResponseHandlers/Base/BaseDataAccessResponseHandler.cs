using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq.Expressions;

namespace RecipeBook.API.ResponseHandlers.Base
{
    public abstract class BaseDataAccessResponseHandler<TReadDto, TCreateDto, TEntity> : ControllerBase, IResponseHandler<TReadDto, TCreateDto, TEntity>
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

        public abstract Task<ActionResult<TReadDto>> CreateEntityAsync(TCreateDto createDto, ModelStateDictionary modelState, string createdAtName);
        public abstract Task<ActionResult<IEnumerable<TReadDto>>> GetAllEntities();
        public abstract Task<ActionResult<TReadDto>> GetEntityByIdAsync(int id);
        public abstract Task<ActionResult<TReadDto>> GetEntityByCriteria(Expression<Func<TEntity, bool>> expression);
        public abstract Task<ActionResult<IEnumerable<TReadDto>>> GetEntitiesByCriteria(Expression<Func<TEntity, bool>> expression);
    }
}
