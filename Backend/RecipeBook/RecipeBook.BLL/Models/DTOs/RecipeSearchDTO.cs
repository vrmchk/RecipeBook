namespace RecipeBook.BLL.Models.DTOs;

public class RecipeSearchDTO
{
    public string Title { get; set; } = string.Empty;
    public string Ingredients { get; set; } = string.Empty;
    public string Servings { get; set; } = string.Empty;
    public string Instructions { get; set; } = string.Empty;
}