namespace RecipeBook.BLL.Exceptions.Base;

public abstract class CustomExceptionBase : ApplicationException
{
    protected CustomExceptionBase(string? cause, string message, Exception? innerException = null)
        : base(message, innerException)
    {
        CauseOfError = cause;
    }

    public string? CauseOfError { get; set; }
}