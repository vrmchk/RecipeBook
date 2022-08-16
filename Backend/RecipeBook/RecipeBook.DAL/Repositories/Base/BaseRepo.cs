using Microsoft.EntityFrameworkCore;
using RecipeBook.DAL.Context;
using RecipeBook.DAL.Entities.Base;
using RecipeBook.DAL.Repositories.Interfaces;

namespace RecipeBook.DAL.Repositories.Base;

public class BaseRepo<T> : IRepo<T> where T : BaseEntity, new()
{
    protected ApplicationContext Context { get; }
    protected DbSet<T> Table { get; }

    public BaseRepo(ApplicationContext context)
    {
        Context = context;
        Table = Context.Set<T>();
    }

    public BaseRepo(DbContextOptions<ApplicationContext> options) : this(new ApplicationContext(options)) { }

    public virtual async Task<IEnumerable<T>> GetAll() => await Table.ToListAsync();

    public virtual async Task<T?> Find(int id) => await Table.FindAsync(id);

    public virtual async Task<int> Add(T entity, bool persist)
    {
        await Table.AddAsync(entity);
        return persist ? await SaveChangesAsync() : 0;
    }

    public virtual async Task<int> Update(T entity, bool persist)
    {
        Table.Update(entity);
        return persist ? await SaveChangesAsync() : 0;
    }

    public virtual async Task<int> Delete(T entity, bool persist)
    {
        Table.Remove(entity);
        return persist ? await SaveChangesAsync() : 0;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await Context.SaveChangesAsync();
    }

    public void Dispose()
    {
        Context.Dispose();
    }
}