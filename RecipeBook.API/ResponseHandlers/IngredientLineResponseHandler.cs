using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RecipeBook.API.Extensions;
using RecipeBook.API.ResponseHandlers.Base;
using RecipeBook.Domain.Models;
using RecipeBook.Infrastructure.MediatR.Commands.IngredientLine;
using RecipeBook.Infrastructure.MediatR.Queries.IngredientLine;
using RecipeBook.Infrastructure.Models.Dtos.IngredientLine;
using RecipeBook.Infrastructure.Models.Dtos.Recipe;
using System.Linq.Expressions;

namespace RecipeBook.API.ResponseHandlers
{
    public class IngredientLineResponseHandler : BaseDataAccessResponseHandler<IngredientLineDto, IngredientLineCreateDto, IngredientLine, GetSingleIngredientDto>
    {
        public IngredientLineResponseHandler(IMapper mapper, IMediator mediator, ILogger<IngredientLineResponseHandler> logger) : base(mapper, mediator, logger)
        {
        }

        public override async Task<ActionResult> CreateEntityAsync(IngredientLineCreateDto createDto, ModelStateDictionary modelState, string createdAtName)
        {
            if (string.IsNullOrEmpty(createDto.Ingredient))
            {
                modelState.AddModelError(nameof(createDto.Ingredient), ModelStateErrors.ModelError.NotEmpty);
            }

            if (createDto.UnitOfMeasurementId <= 0)
            {
                modelState.AddModelError(nameof(createDto.UnitOfMeasurementId), ModelStateErrors.ModelError.InvalidId);
            }

            if(createDto.IngredientAmount <= 0)
            {
                modelState.AddModelError(nameof(createDto.IngredientAmount), ModelStateErrors.ModelError.GreaterThanZero);
            }

            if(createDto.RecipeId <= 0)
            {
                modelState.AddModelError(nameof(createDto.RecipeId), ModelStateErrors.ModelError.InvalidId);
            }

            if (modelState.IsValid)
            {
                var ingredientLine = await Mediator.Send(new CreateIngredienLine()
                {
                    IngredientLine = Mapper.Map<IngredientLine>(createDto)
                });

                if (ingredientLine.Id == 0)
                {
                    return StatusCode(500, "Something went wrong while creating a new " + nameof(IngredientLine));
                }

                return CreatedAtAction(createdAtName, new { id = ingredientLine.Id }, Mapper.Map<IngredientLineDto>(ingredientLine));
            }
            else
            {
                return BadRequest(modelState);
            }
        }

        public override async Task<ActionResult> GetAllEntities()
        {
            return Ok(Mapper.Map<IEnumerable<IngredientLineDto>>(await Mediator.Send(new GetAllIngredientLines())));
        }

        public override async Task<ActionResult> GetEntitiesByCriteria(Expression<Func<IngredientLine, bool>> expression)
        {
            var ingredientLines = await Mediator.Send(new GetIngredientLinesByCriteria()
            {
                Expression = expression
            });

            return Ok(Mapper.Map<IEnumerable<IngredientLineDto>>(ingredientLines));
        }

        public override async Task<ActionResult> GetEntityByCriteria(Expression<Func<IngredientLine, bool>> expression)
        {
            var ingredientLine = await Mediator.Send(new GetIngredientLineByCriteria()
            {
                Expression = expression
            });

            return Ok(Mapper.Map<IngredientLineDto>(ingredientLine));
        }

        public override async Task<ActionResult> GetEntityByIdAsync(GetSingleIngredientDto getSingleIngredientDto)
        {
            var ingredientLine = await Mediator.Send(new GetIngredientLine()
            {
                Id = getSingleIngredientDto.IngredientId
            });

            if(ingredientLine == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<IngredientLineDto>(ingredientLine));
        }
    }
}
