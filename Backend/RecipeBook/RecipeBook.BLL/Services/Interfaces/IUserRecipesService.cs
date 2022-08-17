using RecipeBook.BLL.Models.DTOs;

namespace RecipeBook.BLL.Services.Interfaces;

public interface IUserRecipesService
{
    Task<IEnumerable<RecipeDTO>> GetAllRecipesAsync(int userId);
    Task<RecipeDTO?> GetRecipeAsync(int userId, int recipeId);
    Task<RecipeDTO> AddRecipeAsync(int userId, RecipeCreateDTO dto);
    Task<RecipeDTO> UpdateRecipeAsync(int userId, int recipeId, RecipeUpdateDTO dto);
    Task DeleteRecipeAsync(int userId, int recipeId);
}