using System.ComponentModel.DataAnnotations;

namespace RecipeBook.BLL.Models.DTOs;

public class RecipeUpdateDTO
{
    [Required, MinLength(2)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public IEnumerable<string> Ingredients { get; set; } = new List<string>();

    [Required]
    public int Servings { get; set; }

    [Required, MinLength(1)]
    public string Instructions { get; set; } = string.Empty;

}