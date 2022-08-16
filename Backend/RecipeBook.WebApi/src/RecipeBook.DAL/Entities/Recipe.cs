using RecipeBook.DAL.Entities.Base;

namespace RecipeBook.DAL.Entities;

public class Recipe : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public IEnumerable<string> Ingredients { get; set; } = new List<string>();
    public int Servings { get; set; }
    public string Instructions { get; set; } = string.Empty;
}