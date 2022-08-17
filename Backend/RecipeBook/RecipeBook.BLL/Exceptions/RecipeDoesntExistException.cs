using RecipeBook.BLL.Exceptions.Base;

namespace RecipeBook.BLL.Exceptions;

public class RecipeDoesntExistException : CustomExceptionBase
{
    public RecipeDoesntExistException(string? cause, string message, Exception? innerException = null)
        : base(cause, message, innerException) { }
}