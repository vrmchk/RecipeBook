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
        return await Table.FirstOrDefaultAsync(user => user.Email == email);
    }
}