using AutoMapper;
using RecipeBook.BLL.Extensions;
using RecipeBook.BLL.Models.DTOs;

namespace RecipeBook.BLL.Profiles;

public class RecipeProfile : Profile
{
    public RecipeProfile()
    {
        CreateMap<RecipeSearchDTO, RecipeDTO>()
            .ForMember(dest => dest.Ingredients,
                options => options.MapFrom(searchDto => searchDto.IngredientsToEnumerable()))
            .ForMember(dest => dest.Servings,
                options => options.MapFrom(searchDto => searchDto.ServingsToInt()));
    }
}