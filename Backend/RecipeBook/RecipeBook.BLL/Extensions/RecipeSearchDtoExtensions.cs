using RecipeBook.BLL.Models.DTOs;

namespace RecipeBook.BLL.Extensions;

public static class RecipeSearchDtoExtensions
{
    public static IEnumerable<string> IngredientsToEnumerable(this RecipeSearchDto source) =>
        source.Ingredients.Split("|", StringSplitOptions.RemoveEmptyEntries);
    
    public static int ServingsToInt(this RecipeSearchDto source) =>
        Convert.ToInt32(source.Servings.Split(" ", StringSplitOptions.RemoveEmptyEntries).First());
}