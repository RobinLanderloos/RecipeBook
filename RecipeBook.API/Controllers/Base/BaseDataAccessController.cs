using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RecipeBook.API.ResponseHandlers;

namespace RecipeBook.API.Controllers.Base
{
    [ApiController]
    public abstract class BaseDataAccessController<TReadDto, TCreateDto, TEntity, TGetSingleDto> : ControllerBase
    {
        protected readonly IMapper Mapper;
        protected readonly ILogger Logger;
        protected readonly IResponseHandler<TReadDto, TCreateDto, TEntity, TGetSingleDto> ResponseHandler;

        public BaseDataAccessController(IMapper mapper, ILogger logger, IResponseHandler<TReadDto, TCreateDto, TEntity, TGetSingleDto> responseHandler)
        {
            Mapper = mapper;
            Logger = logger;
            ResponseHandler = responseHandler;
        }
    }
}
