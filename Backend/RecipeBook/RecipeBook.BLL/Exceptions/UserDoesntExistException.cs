using RecipeBook.BLL.Exceptions.Base;

namespace RecipeBook.BLL.Exceptions;

public class UserDoesntExistException : CustomExceptionBase
{
    public UserDoesntExistException(string? cause, string message, Exception? innerException = null)
        : base(cause, message, innerException) { }
}