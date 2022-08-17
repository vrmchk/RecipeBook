using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RecipeBook.BLL.Configure;
using RecipeBook.BLL.Models.Auth;
using RecipeBook.BLL.Services.Interfaces;
using RecipeBook.DAL.Entities;
using RecipeBook.DAL.Repositories.Interfaces;

namespace RecipeBook.BLL.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepo _userRepo;
    private readonly IPasswordHasher _passwordHasher;
    private readonly JwtConfig _jwtConfig;

    public AuthService(IUserRepo userRepo, IPasswordHasher passwordHasher, IOptionsMonitor<JwtConfig> optionsMonitor)
    {
        _userRepo = userRepo;
        _passwordHasher = passwordHasher;
        _jwtConfig = optionsMonitor.CurrentValue;
    }

    public async Task<AuthResult> RegisterAsync(UserRegisterRequest registerUser)
    {
        var existingUser = await _userRepo.FindByEmailAsync(registerUser.Email);
        if (existingUser != null)
            return new AuthResult {Success = false, Errors = new[] {"Email already registered"}};

        string salt = _passwordHasher.GenerateSalt();
        string passwordHash = _passwordHasher.HashPassword(registerUser.Password, salt);
        var newUser = new User {Email = registerUser.Email, Salt = salt, PasswordHash = passwordHash};

        bool created = await _userRepo.AddAsync(newUser) > 0;
        if (!created)
            return new AuthResult {Success = false, Errors = new[] {"Unable to create a new user"}};

        return new AuthResult {Success = true, Token = GenerateJwtToken(newUser)};
    }

    public async Task<AuthResult> LoginAsync(UserLoginRequest loginUser)
    {
        var existingUser = await _userRepo.FindByEmailAsync(loginUser.Email);
        if (existingUser == null)
            return new AuthResult {Success = false, Errors = new[] {"Invalid email address"}};

        bool passwordCorrect = existingUser.PasswordHash ==
                               _passwordHasher.HashPassword(loginUser.Password, existingUser.Salt);
        if (!passwordCorrect)
            return new AuthResult {Success = false, Errors = new[] {"Invalid password"}};

        return new AuthResult {Success = true, Token = GenerateJwtToken(existingUser)};
    }

    private string GenerateJwtToken(User user)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor {
            Subject = new ClaimsIdentity(new[] {
                new Claim("Id", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(6),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = jwtTokenHandler.CreateToken(tokenDescriptor);
        string jwtToken = jwtTokenHandler.WriteToken(token);
        return jwtToken;
    }
}