using Microsoft.AspNetCore.Http;
using RecipeBook.BLL.Exceptions;

namespace RecipeBook.BLL.Extensions;

public static class HttpContextExtensions
{
    public static int GetUserId(this HttpContext httpContext)
    {
        var claim = httpContext.User.Claims.FirstOrDefault(c => c.Type == "Id");
        if (claim == null)
            throw new UserDoesntExistException(nameof(claim), $"This user doesn't exist");

        return Convert.ToInt32(claim.Value);
    }
}