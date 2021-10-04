using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace RecipeBook.API.ResponseHandlers
{
    public interface IResponseHandler<TReadDto, TCreateDto>
    {
        Task<ActionResult<TReadDto>> CreateRecipe(TCreateDto recipeCreateDto, ModelStateDictionary modelState, string createdAtName);
        Task<ActionResult<IEnumerable<TReadDto>>> GetAllRecipes();
        Task<ActionResult<TReadDto>> GetRecipeById(int id);
    }
}
