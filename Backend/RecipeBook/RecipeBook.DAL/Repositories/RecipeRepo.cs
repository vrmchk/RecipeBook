using Microsoft.EntityFrameworkCore;
using RecipeBook.DAL.Context;
using RecipeBook.DAL.Entities;
using RecipeBook.DAL.Repositories.Base;
using RecipeBook.DAL.Repositories.Interfaces;

namespace RecipeBook.DAL.Repositories;

public class RecipeRepo : BaseRepo<Recipe>, IRecipeRepo
{
    public RecipeRepo(ApplicationContext context) : base(context) { }
    internal RecipeRepo(DbContextOptions<ApplicationContext> options) : base(options) { }

    public async Task<Recipe?> FindByTitle(string title)
    {
        return await Table.FirstOrDefaultAsync(recipe => recipe.Title == title);
    }
}