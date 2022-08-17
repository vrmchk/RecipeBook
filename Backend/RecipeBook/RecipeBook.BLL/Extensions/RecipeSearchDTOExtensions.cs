using RecipeBook.BLL.Models.DTOs.Search;

namespace RecipeBook.BLL.Extensions;

public static class RecipeSearchDTOExtensions
{
    public static IEnumerable<string> IngredientsToEnumerable(this RecipeSearchDTO source) =>
        source.Ingredients.Split("|", StringSplitOptions.RemoveEmptyEntries);
    
    public static int ServingsToInt(this RecipeSearchDTO source) =>
        Convert.ToInt32(source.Servings.Split(" ", StringSplitOptions.RemoveEmptyEntries).First());
}