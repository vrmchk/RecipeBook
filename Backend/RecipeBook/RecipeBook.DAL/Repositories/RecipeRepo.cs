using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using RecipeBook.DAL.Context;
using RecipeBook.DAL.Entities;
using RecipeBook.DAL.Extensions;
using RecipeBook.DAL.Repositories.Base;
using RecipeBook.DAL.Repositories.Interfaces;

namespace RecipeBook.DAL.Repositories;

public class RecipeRepo : BaseRepo<Recipe>, IRecipeRepo
{
    public RecipeRepo(ApplicationContext context) : base(context) { }
    internal RecipeRepo(DbContextOptions<ApplicationContext> options) : base(options) { }

    public override async Task<IEnumerable<Recipe>> GetAllAsync()
    {
        return await Task.Run(() => Table
            .Include(r => r.Ingredients)
            .Include(r => r.UserNavigation));
    }

    public override async Task<Recipe?> FindAsync(int id)
    {
        var recipe = await Table
            .Where(r => r.Id == id)
            .Include(r => r.Ingredients)
            .Include(r => r.UserNavigation)
            .FirstOrDefaultAsync();
        return recipe;
    }

    public override async Task<Recipe?> FindAsNoTrackingAsync(int id)
    {
        return await Table
            .AsNoTracking()
            .Where(r => r.Id == id)
            .Include(r => r.Ingredients)
            .Include(r => r.UserNavigation)
            .FirstOrDefaultAsync();
    }
}