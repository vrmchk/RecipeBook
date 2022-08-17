using RecipeBook.BLL.Models.Auth;

namespace RecipeBook.BLL.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResult> RegisterAsync(UserRegisterRequest registerUser);
    Task<AuthResult> LoginAsync(UserLoginRequest loginUser);
}