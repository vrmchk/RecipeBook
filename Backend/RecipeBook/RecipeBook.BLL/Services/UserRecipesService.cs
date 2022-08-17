using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecipeBook.BLL.Models.DTOs;
using RecipeBook.BLL.Services.Interfaces;
using RecipeBook.DAL.Entities;
using RecipeBook.DAL.Repositories.Interfaces;

namespace RecipeBook.BLL.Services;

public class UserRecipesService : IUserRecipesService
{
    private readonly IUserRepo _userRepo;
    private readonly IRecipeRepo _recipeRepo;
    private readonly IMapper _mapper;

    public UserRecipesService(IUserRepo userRepo, IRecipeRepo recipeRepo, IMapper mapper)
    {
        _userRepo = userRepo;
        _recipeRepo = recipeRepo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RecipeDTO>> GetAllRecipesAsync(int userId)
    {
        var user = await _userRepo.FindAsNoTrackingAsync(userId);
        if (user == null)
            return Enumerable.Empty<RecipeDTO>();

        var recipes = _recipeRepo.Table
            .Include(r => r.UserNavigation)
            .Where(r => r.UserNavigation.Id == userId)
            .Include(r => r.Ingredients);
        return _mapper.Map<IEnumerable<RecipeDTO>>(recipes);
    }

    public async Task<RecipeDTO?> GetRecipeAsync(int userId, int recipeId)
    {
        var user = await _userRepo.FindAsNoTrackingAsync(userId);
        if (user == null)
            return null;

        var recipe = await _recipeRepo.Table
            .Include(r => r.UserNavigation)
            .Where(r => r.UserNavigation.Id == userId)
            .Include(r => r.Ingredients)
            .FirstOrDefaultAsync(r => r.Id == recipeId);
        return _mapper.Map<RecipeDTO>(recipe);
    }

    public async Task<RecipeDTO> AddRecipeAsync(int userId, RecipeCreateDTO dto)
    {
        var user = await _userRepo.FindAsNoTrackingAsync(userId);
        if (user == null)
            throw new Exception(nameof(user));
        //replace with custom exception
        var recipe = _mapper.Map<Recipe>(dto);
        recipe.UserId = userId;
        await _recipeRepo.AddAsync(recipe);
        return _mapper.Map<RecipeDTO>(recipe);
    }

    public async Task<RecipeDTO> UpdateRecipeAsync(int userId, int recipeId, RecipeUpdateDTO dto)
    {
        var user = await _userRepo.FindAsNoTrackingAsync(userId);
        if (user == null)
            throw new Exception(nameof(user));

        if (recipeId != dto.Id)
            throw new Exception(nameof(dto));
        //replace with custom exception

        bool ownsRecipe = await UserOwnsRecipe(userId, recipeId);
        if (!ownsRecipe)
            throw new Exception(nameof(ownsRecipe));

        var newRecipe = _mapper.Map<Recipe>(dto);
        var existingRecipe = await _recipeRepo.FindAsync(recipeId);
        if (existingRecipe == null)
            throw new Exception(nameof(existingRecipe));

        existingRecipe.Ingredients = newRecipe.Ingredients;
        existingRecipe.Instructions = newRecipe.Instructions;
        existingRecipe.Servings = newRecipe.Servings;
        existingRecipe.Title = newRecipe.Title;

        await _recipeRepo.UpdateAsync(existingRecipe);
        return _mapper.Map<RecipeDTO>(newRecipe);
    }

    public async Task DeleteRecipeAsync(int userId, int recipeId)
    {
        var user = await _userRepo.FindAsNoTrackingAsync(userId);
        if (user == null)
            throw new Exception(nameof(user));

        bool ownsRecipe = await UserOwnsRecipe(userId, recipeId);
        if (!ownsRecipe)
            throw new Exception(nameof(ownsRecipe));

        var recipe = await _recipeRepo.FindAsync(recipeId);
        if (recipe == null)
            throw new Exception(nameof(recipe));

        await _recipeRepo.DeleteAsync(recipe);
    }

    private async Task<bool> UserOwnsRecipe(int userId, int recipeId)
    {
        var recipe = await _recipeRepo.FindAsNoTrackingAsync(recipeId);

        if (recipe == null || recipe.UserId != userId)
            return false;

        return true;
    }
}