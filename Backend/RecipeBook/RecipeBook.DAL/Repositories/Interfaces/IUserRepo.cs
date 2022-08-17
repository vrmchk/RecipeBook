using RecipeBook.DAL.Entities;

namespace RecipeBook.DAL.Repositories.Interfaces;

public interface IUserRepo : IRepo<User>
{
    Task<User?> FindByEmailAsync(string email);
}