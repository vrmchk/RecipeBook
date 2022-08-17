namespace RecipeBook.BLL.Models.DTOs.Recipe;

public class RecipeSearchDto
{
    public string Title { get; set; } = string.Empty;
    public string Ingredients { get; set; } = string.Empty;
    public string Servings { get; set; } = string.Empty;
    public string Instructions { get; set; } = string.Empty;
}