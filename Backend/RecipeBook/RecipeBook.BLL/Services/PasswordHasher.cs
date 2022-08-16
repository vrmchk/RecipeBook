using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using RecipeBook.BLL.Services.Interfaces;

namespace RecipeBook.BLL.Services;

public class PasswordHasher : IPasswordHasher
{
    public string GenerateSalt(int nSalt) => Convert.ToBase64String(RandomNumberGenerator.GetBytes(nSalt));

    public string HashPassword(string password, string salt)
    {
        byte[] saltBytes = Convert.FromBase64String(salt);
        return Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: saltBytes,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 32));
    }
}