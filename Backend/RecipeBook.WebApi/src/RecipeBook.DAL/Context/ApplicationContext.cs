using Microsoft.EntityFrameworkCore;
using RecipeBook.DAL.Entities;

namespace RecipeBook.DAL.Context;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public virtual DbSet<Recipe> Recipes { get; set; } = null!;
}