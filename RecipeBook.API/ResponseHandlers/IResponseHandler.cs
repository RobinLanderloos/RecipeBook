using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq.Expressions;

namespace RecipeBook.API.ResponseHandlers
{
    public interface IResponseHandler<TReadDto, TCreateDto, TEntity, TGetSingleDto>
    {
        Task<ActionResult> CreateEntityAsync(TCreateDto recipeCreateDto, ModelStateDictionary modelState, string createdAtName);
        Task<ActionResult> GetAllEntities();
        Task<ActionResult> GetEntityByIdAsync(TGetSingleDto getSingleDto);
        Task<ActionResult> GetEntitiesByCriteria(Expression<Func<TEntity, bool>> expression);
        Task<ActionResult> GetEntityByCriteria(Expression<Func<TEntity, bool>> expression);
    }
}
