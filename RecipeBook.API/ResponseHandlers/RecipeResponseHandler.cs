﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RecipeBook.API.ResponseHandlers.Base;
using RecipeBook.Domain.Models;
using RecipeBook.Infrastructure.MediatR.Commands.Recipe;
using RecipeBook.Infrastructure.MediatR.Queries.Recipe;
using RecipeBook.Infrastructure.Models.Dtos;

namespace RecipeBook.API.ResponseHandlers
{
    public class RecipeResponseHandler : BaseDataAccessResponseHandler<RecipeDto, RecipeCreateDto>
    {
        public RecipeResponseHandler(IMapper mapper, IMediator mediator, ILogger<RecipeResponseHandler> logger) : base(mapper, mediator, logger)
        {
        }

        public override async Task<ActionResult<RecipeDto>> CreateRecipe(RecipeCreateDto recipeCreateDto, ModelStateDictionary modelState, string createdAtName)
        {
            if (!recipeCreateDto.Ingredients.Any())
            {
                modelState.AddModelError(nameof(recipeCreateDto.Ingredients), $"{nameof(recipeCreateDto.Ingredients)} cannot be empty");
            }

            if (recipeCreateDto.Servings <= 0)
            {
                modelState.AddModelError(nameof(recipeCreateDto.Servings), $"{nameof(recipeCreateDto.Servings)} must be greater than 0");
            }

            if (modelState.IsValid)
            {
                var recipe = await Mediator.Send(new CreateRecipe(Mapper.Map<Recipe>(recipeCreateDto)));

                if (recipe.Id == 0)
                {
                    return StatusCode(500, "Something went wrong while creating a new " + nameof(Recipe));
                }

                return CreatedAtAction(createdAtName, new { id = recipe.Id }, Mapper.Map<RecipeDto>(recipe));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        public override async Task<ActionResult<IEnumerable<RecipeDto>>> GetAllRecipes()
        {
            var recipes = await Mediator.Send(new GetAllRecipes());

            return Ok(Mapper.Map<IEnumerable<RecipeDto>>(recipes));
        }

        public override async Task<ActionResult<RecipeDto>> GetRecipeById(int id)
        {
            var recipe = await Mediator.Send(new GetRecipe(id));

            if (recipe == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<RecipeDto>(recipe));
        }
    }
}
