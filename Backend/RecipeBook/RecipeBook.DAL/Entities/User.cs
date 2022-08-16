using RecipeBook.DAL.Entities.Base;

namespace RecipeBook.DAL.Entities;

public class User : BaseEntity
{
    public string Email { get; set; } = string.Empty;
    public bool EmailConfirmed { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
    public string Salt { get; set; } = string.Empty;
    public IEnumerable<Recipe> Recipes { get; set; } = new List<Recipe>();
}