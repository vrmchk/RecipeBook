using RecipeBook.DAL.Entities.Base;

namespace RecipeBook.DAL.Entities;

public class Ingredient : BaseEntity
{
    public string Name { get; set; } = string.Empty;
}