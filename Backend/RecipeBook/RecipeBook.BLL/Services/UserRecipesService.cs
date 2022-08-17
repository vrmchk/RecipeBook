using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecipeBook.BLL.Exceptions;
using RecipeBook.BLL.Models.DTOs;
using RecipeBook.BLL.Services.Interfaces;
using RecipeBook.DAL.Entities;
using RecipeBook.DAL.Repositories.Interfaces;

namespace RecipeBook.BLL.Services;

public class UserRecipesService : IUserRecipesService
{
    private readonly IRecipeRepo _recipeRepo;
    private readonly IMapper _mapper;

    public UserRecipesService(IRecipeRepo recipeRepo, IMapper mapper)
    {
        _recipeRepo = recipeRepo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RecipeDTO>> GetAllRecipesAsync(int userId)
    {
        var recipes = await Task.Run(() => _recipeRepo.Table
            .Include(r => r.UserNavigation)
            .Where(r => r.UserNavigation.Id == userId)
            .Include(r => r.Ingredients));
        return _mapper.Map<IEnumerable<RecipeDTO>>(recipes);
    }

    public async Task<RecipeDTO> GetRecipeAsync(int userId, int recipeId)
    {
        var recipe = await _recipeRepo.Table
            .Include(r => r.UserNavigation)
            .Where(r => r.UserNavigation.Id == userId)
            .Include(r => r.Ingredients)
            .FirstOrDefaultAsync(r => r.Id == recipeId);
        if (recipe == null)
            throw new RecipeDoesntExistException(nameof(recipe), $"Recipe with id {recipeId} doesn't exist");
        return _mapper.Map<RecipeDTO>(recipe);
    }

    public async Task<RecipeDTO> AddRecipeAsync(int userId, RecipeCreateDTO dto)
    {
        var recipe = _mapper.Map<Recipe>(dto);
        recipe.UserId = userId;
        await _recipeRepo.AddAsync(recipe);
        return _mapper.Map<RecipeDTO>(recipe);
    }

    public async Task<RecipeDTO> UpdateRecipeAsync(int userId, int recipeId, RecipeUpdateDTO dto)
    {
        bool ownsRecipe = await UserOwnsRecipe(userId, recipeId);
        if (!ownsRecipe)
            throw new RecipeDoesntExistException(nameof(recipeId), $"Recipe with id {recipeId} doesn't exist");
        
        var existingRecipe = await _recipeRepo.FindAsync(recipeId);
        _mapper.Map(dto, existingRecipe);
        await _recipeRepo.UpdateAsync(existingRecipe!);
        return _mapper.Map<RecipeDTO>(existingRecipe);
    }

    public async Task DeleteRecipeAsync(int userId, int recipeId)
    {
        bool ownsRecipe = await UserOwnsRecipe(userId, recipeId);
        if (!ownsRecipe)
            throw new RecipeDoesntExistException(nameof(recipeId), $"Recipe with id {recipeId} doesn't exist");

        var recipe = await _recipeRepo.FindAsync(recipeId);
        await _recipeRepo.DeleteAsync(recipe!);
    }

    private async Task<bool> UserOwnsRecipe(int userId, int recipeId)
    {
        var recipe = await _recipeRepo.FindAsNoTrackingAsync(recipeId);
        if (recipe == null || recipe.UserId != userId)
            return false;

        return true;
    }
}