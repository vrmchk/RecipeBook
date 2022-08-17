namespace RecipeBook.DAL.Extensions;

public static class StringExtensions
{
    public static bool ContainsIgnoreCase(this string source, string value)
        => source.ToLower().Contains(value.ToLower());
}