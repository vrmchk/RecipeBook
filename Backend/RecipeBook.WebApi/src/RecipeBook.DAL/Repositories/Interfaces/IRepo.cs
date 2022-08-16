namespace RecipeBook.DAL.Repositories.Interfaces;

public interface IRepo<T> : IDisposable
{
    Task<IEnumerable<T>> GetAll();
    Task<T?> Find(int id);
    Task<int> Add(T entity, bool persist);
    Task<int> Update(T entity, bool persist);
    Task<int> Delete(T entity, bool persist);
    Task<int> SaveChangesAsync();
}