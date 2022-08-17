using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using RecipeBook.BLL.Exceptions;

namespace RecipeBook.BLL.Filters;

public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly IWebHostEnvironment _hostEnvironment;

    public CustomExceptionFilterAttribute(IWebHostEnvironment hostEnvironment)
    {
        _hostEnvironment = hostEnvironment;
    }

    public override void OnException(ExceptionContext context)
    {
        var ex = context.Exception;
        string message = ex.Message;
        string stackTrace = string.Empty;
        if (_hostEnvironment.IsDevelopment())
            stackTrace = context.Exception.StackTrace ?? string.Empty;

        IActionResult actionResult = ex switch {
            SearchFailedException => new BadRequestObjectResult(new {
                Error = "Search issue", Message = message, StackTrace = stackTrace
            }),
            UserDoesntExistException => new BadRequestObjectResult(new {
                Error = "User issue", Message = message, StackTrace = stackTrace
            }),
            RecipeDoesntExistException => new BadRequestObjectResult(new {
                Error = "Recipe issue", Message = message, StackTrace = stackTrace
            }),
            _ => new ObjectResult(new {Error = "General error", Message = message, StackTrace = stackTrace}) {
                StatusCode = 500
            }
        };
        context.ExceptionHandled = true;
        context.Result = actionResult;
    }
}