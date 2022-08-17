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

    public async Task<IEnumerable<Recipe>> FindByTitleAsync(string title)
    {
        return await Task.Run(() => Table
            .Where(r => r.Title.ContainsIgnoreCase(title))
            .Include(r => r.Ingredients)
            .Include(r => r.UsersNavigation));
    }

    public override async Task<IEnumerable<Recipe>> GetAllAsync()
    {
        return await Task.Run(() => Table
            .Include(r => r.Ingredients)
            .Include(r => r.UsersNavigation));
    }

    public override async Task<Recipe?> FindAsync(int id)
    {
        return await Table
            .Where(r => r.Id == id)
            .Include(r => r.Ingredients)
            .Include(r => r.UsersNavigation)
            .FirstOrDefaultAsync();
    }

    public override async Task<Recipe?> FindAsNoTrackingAsync(int id)
    {
        return await Table
            .AsNoTracking()
            .Where(r => r.Id == id)
            .Include(r => r.Ingredients)
            .Include(r => r.UsersNavigation)
            .FirstOrDefaultAsync();
    }
}