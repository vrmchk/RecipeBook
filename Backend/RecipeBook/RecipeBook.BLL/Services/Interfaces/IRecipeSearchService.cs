using RecipeBook.BLL.Models.DTOs;

namespace RecipeBook.BLL.Services.Interfaces;

public interface IRecipeSearchService
{
    Task<IEnumerable<RecipeDto>> SearchAsync(string title);
}