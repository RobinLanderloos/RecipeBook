using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.API.Extensions
{
    public static class ModelStateDictionaryExtensions
    {
        public static void AddModelError(this ModelStateDictionary modelState, string propertyName, ModelStateErrors.ModelError modelError)
        {
            modelState.AddModelError(propertyName, $"{propertyName} {ModelStateErrors.GetModelError(modelError)}");
        }        

        public static void AddCustomModelError(this ModelStateDictionary modelState, string propertyName, string customError)
        {
            modelState.AddModelError(propertyName, $"{propertyName} {customError}");
        }
    }

    public static class ModelStateErrors
    {
        private static readonly string _invalidIdErrorMessage = "must contain a valid id";
        private static readonly string _notEmptyErrorMessage = "cannot be empty";
        private static readonly string _mustBeGreaterThanZero = "must be greater than 0";

        public static string GetModelError(ModelError modelError)
        {
            return modelError switch
            {
                ModelError.InvalidId => _invalidIdErrorMessage,
                ModelError.NotEmpty => _notEmptyErrorMessage,
                ModelError.GreaterThanZero => _mustBeGreaterThanZero,
                _ => throw new ArgumentException(nameof(modelError)),
            };
        }

        public enum ModelError
        {
            InvalidId,
            NotEmpty,
            GreaterThanZero
        }
    }
}
