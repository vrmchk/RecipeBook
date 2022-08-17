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
    public virtual DbSet<Ingredient> Ingredients { get; set; } = null!;
    public virtual DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(user => user.Recipes)
            .WithMany(recipe => recipe.UsersNavigation);
    }
}