using System.ComponentModel.DataAnnotations;

namespace RecipeBook.BLL.Models.Auth;

public class UserRegisterRequest
{
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required, StringLength(15, MinimumLength = 8)]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&\.,:;])[A-Za-z\d@$!%*#?&\.,:;]+$")]
    public string Password { get; set; } = string.Empty;
}