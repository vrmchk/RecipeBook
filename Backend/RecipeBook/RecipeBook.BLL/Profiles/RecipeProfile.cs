using AutoMapper;
using RecipeBook.BLL.Models.DTOs;
using RecipeBook.DAL.Entities;

namespace RecipeBook.BLL.Profiles;

public class RecipeProfile : Profile
{
    public RecipeProfile()
    {
        CreateMap<RecipeCreateDTO, Recipe>()
            .ForMember(dest => dest.Ingredients,
                options => options.MapFrom(dto => dto.Ingredients.Select(s => new Ingredient {Name = s})));

        CreateMap<RecipeUpdateDTO, Recipe>()
            .ForMember(dest => dest.Ingredients,
                options => options.MapFrom(dto => dto.Ingredients.Select(s => new Ingredient {Name = s})));

        CreateMap<Recipe, RecipeDTO>()
            .ForMember(dest => dest.Ingredients,
                options => options.MapFrom(r => r.Ingredients.Select(i => i.Name)));
    }
}