using RecipeBook.BLL.DTOs.Recipe;

namespace RecipeBook.BLL.Services.Interfaces;

public interface IRecipeSearchService
{
    Task<IEnumerable<RecipeDto>> Search(string title);
}