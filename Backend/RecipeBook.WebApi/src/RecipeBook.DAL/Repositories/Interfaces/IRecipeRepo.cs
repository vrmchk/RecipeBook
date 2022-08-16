﻿using RecipeBook.DAL.Entities;

namespace RecipeBook.DAL.Repositories.Interfaces;

public interface IRecipeRepo : IRepo<Recipe>
{
    Task<Recipe?> FindByTitle(string title);
}