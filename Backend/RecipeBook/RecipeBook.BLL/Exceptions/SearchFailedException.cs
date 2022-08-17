using RecipeBook.BLL.Exceptions.Base;

namespace RecipeBook.BLL.Exceptions;

public class SearchFailedException : CustomExceptionBase
{
    public SearchFailedException(string? cause, string message, Exception? innerException = null)
        : base(cause, message, innerException) { }
}