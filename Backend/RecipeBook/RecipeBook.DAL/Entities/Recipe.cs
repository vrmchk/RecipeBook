using RecipeBook.DAL.Entities.Base;

namespace RecipeBook.DAL.Entities;

public class Recipe : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public IEnumerable<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    public int Servings { get; set; }
    public string Instructions { get; set; } = string.Empty;
    public IEnumerable<User> UsersNavigation { get; set; } = new List<User>();
}