using AutoMapper;
using RecipeBook.BLL.Extensions;
using RecipeBook.BLL.Models.DTOs.Search;

namespace RecipeBook.BLL.Profiles;

public class RecipeSearchProfile : Profile
{
    public RecipeSearchProfile()
    {
        CreateMap<RecipeSearchDTO, RecipeDisplayDTO>()
            .ForMember(dest => dest.Ingredients,
                options => options.MapFrom(searchDto => searchDto.IngredientsToEnumerable()))
            .ForMember(dest => dest.Servings,
                options => options.MapFrom(searchDto => searchDto.ServingsToInt()));
    }
}