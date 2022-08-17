using Microsoft.EntityFrameworkCore;
using RecipeBook.DAL.Context;
using RecipeBook.DAL.Entities;
using RecipeBook.DAL.Repositories.Base;
using RecipeBook.DAL.Repositories.Interfaces;

namespace RecipeBook.DAL.Repositories;

public class UserRepo : BaseRepo<User>, IUserRepo
{
    public UserRepo(ApplicationContext context) : base(context) { }
    internal UserRepo(DbContextOptions<ApplicationContext> options) : base(options) { }

    public async Task<User?> FindByEmailAsync(string email)
    {
        return await Table
            .Where(u => u.Email == email)
            .Include(u => u.Recipes)
            .FirstOrDefaultAsync();
    }

    public override async Task<IEnumerable<User>> GetAllAsync()
    {
        return await Task.Run(() => Table
            .Include(u => u.Recipes));
    }

    public override async Task<User?> FindAsync(int id)
    {
        return await Table
            .Where(r => r.Id == id)
            .Include(u => u.Recipes)
            .FirstOrDefaultAsync();
    }

    public override async Task<User?> FindAsNoTrackingAsync(int id)
    {
        return await Table
            .AsNoTracking()
            .Where(r => r.Id == id)
            .Include(u => u.Recipes)
            .FirstOrDefaultAsync();
    }
}