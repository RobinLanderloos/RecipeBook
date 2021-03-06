using RecipeBook.Domain.Models;
using RecipeBook.Infrastructure.Models.Dtos.IngredientLine;
using RecipeBook.Infrastructure.Models.Dtos.Recipe;
using RecipeBook.Infrastructure.Models.Dtos.UnitOfMeasurement;

namespace RecipeBook.Infrastructure.Profiles
{
    public class RecipeBook : BaseProfile
    {
        public RecipeBook()
        {
            CreateTwoWayMapping<RecipeDto, Recipe>();
            CreateMap<IngredientLine, IngredientLineDto>()
                .AfterMap((src, dest) => dest.UnitOfMeasurement = src.UnitOfMeasurement.Name);
            CreateMap<IngredientLineDto, IngredientLine>();
            CreateTwoWayMapping<UnitOfMeasurementDto, UnitOfMeasurement>();
            CreateTwoWayMapping<RecipeCreateDto, Recipe>();
            CreateTwoWayMapping<IngredientLineCreateDto, IngredientLine>();
        }
    }
}
