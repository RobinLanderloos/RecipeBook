using AutoMapper;

namespace RecipeBook.Infrastructure.Profiles
{
    public abstract class BaseProfile : Profile
    {
        protected void CreateTwoWayMapping<TSource, TDestination>()
        {
            CreateMap<TSource, TDestination>();
            CreateMap<TDestination, TSource>();
        }
    }
}
