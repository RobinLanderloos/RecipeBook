using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq.Expressions;

namespace RecipeBook.API.ResponseHandlers
{
    public interface IResponseHandler<TReadDto, TCreateDto, TEntity>
    {
        Task<ActionResult<TReadDto>> CreateEntityAsync(TCreateDto recipeCreateDto, ModelStateDictionary modelState, string createdAtName);
        Task<ActionResult<IEnumerable<TReadDto>>> GetAllEntities();
        Task<ActionResult<TReadDto>> GetEntityByIdAsync(int id);
        Task<ActionResult<IEnumerable<TReadDto>>> GetEntitiesByCriteria(Expression<Func<TEntity, bool>> expression);
        Task<ActionResult<TReadDto>> GetEntityByCriteria(Expression<Func<TEntity, bool>> expression);
    }
}
