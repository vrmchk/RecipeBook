using RecipeBook.BLL.Models.DTOs;
using RecipeBook.BLL.Models.DTOs.Search;

namespace RecipeBook.BLL.Services.Interfaces;

public interface IRecipeSearchService
{
    Task<IEnumerable<RecipeDisplayDTO>> SearchAsync(string title);
}