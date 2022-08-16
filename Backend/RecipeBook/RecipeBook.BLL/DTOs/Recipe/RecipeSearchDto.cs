using System.Text.Json.Serialization;

namespace RecipeBook.BLL.DTOs.Recipe;

public class RecipeSearchDto
{
    public string Title { get; set; }

    public string Ingredients { get; set; }

    public string Servings { get; set; }

    public string Instructions { get; set; }
}