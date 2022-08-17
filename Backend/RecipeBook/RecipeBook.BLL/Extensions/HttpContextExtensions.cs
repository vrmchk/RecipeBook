using Microsoft.AspNetCore.Http;

namespace RecipeBook.BLL.Extensions;

public static class HttpContextExtensions
{
    public static int GetUserId(this HttpContext httpContext)
    {
        var claim = httpContext.User.Claims.FirstOrDefault(c => c.Type == "Id");
        if (claim == null)
            throw new Exception(nameof(claim));
        //replace with custom exception
        return Convert.ToInt32(claim.Value);
    }
}