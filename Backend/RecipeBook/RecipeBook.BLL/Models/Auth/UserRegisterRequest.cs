using System.ComponentModel.DataAnnotations;

namespace RecipeBook.BLL.Models.Auth;

public class UserRegisterRequest
{
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    //add regular expression
    [Required, MinLength(8)]
    public string Password { get; set; } = string.Empty;
}