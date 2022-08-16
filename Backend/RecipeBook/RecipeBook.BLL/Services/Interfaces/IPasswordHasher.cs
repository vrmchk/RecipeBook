namespace RecipeBook.BLL.Services.Interfaces;

public interface IPasswordHasher
{
    string GenerateSalt(int nSalt);
    string HashPassword(string password, string salt);
}