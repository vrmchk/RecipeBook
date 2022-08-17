using System.ComponentModel.DataAnnotations.Schema;
using RecipeBook.DAL.Entities.Base;

namespace RecipeBook.DAL.Entities;

public class Recipe : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public IEnumerable<Ingredient> Ingredients { get; set; } = null!;
    public int Servings { get; set; }
    public string Instructions { get; set; } = string.Empty;
    public int UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User UserNavigation { get; set; } = null!;
}