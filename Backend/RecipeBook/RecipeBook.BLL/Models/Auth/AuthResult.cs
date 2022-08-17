namespace RecipeBook.BLL.Models.Auth;

public class AuthResult
{
    public string Token { get; set; } = string.Empty;
    public bool Success { get; set; }
    public IEnumerable<string> Errors { get; set; } = new List<string>();
}