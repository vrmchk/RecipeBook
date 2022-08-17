namespace RecipeBook.BLL.Models.DTOs.Recipe;

public class RecipeDto
{
    public string Title { get; set; } = string.Empty;
    public IEnumerable<string> Ingredients { get; set; } = new List<string>();
    public int Servings { get; set; }
    public string Instructions { get; set; } = string.Empty;
}