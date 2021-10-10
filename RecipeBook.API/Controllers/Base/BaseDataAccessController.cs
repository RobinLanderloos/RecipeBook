﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RecipeBook.API.ResponseHandlers;

namespace RecipeBook.API.Controllers.Base
{
    [ApiController]
    public abstract class BaseDataAccessController<TReadDto, TCreateDto, TEntity> : ControllerBase
    {
        protected readonly IMediator Mediator;
        protected readonly IMapper Mapper;
        protected readonly ILogger Logger;
        protected readonly IResponseHandler<TReadDto, TCreateDto, TEntity> ResponseHandler;

        public BaseDataAccessController(IMediator mediator, IMapper mapper, ILogger logger, IResponseHandler<TReadDto, TCreateDto, TEntity> responseHandler)
        {
            Mediator = mediator;
            Mapper = mapper;
            Logger = logger;
            ResponseHandler = responseHandler;
        }
    }
}
