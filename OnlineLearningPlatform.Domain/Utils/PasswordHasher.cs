using System.Security.Cryptography;
using System.Text;

namespace OnlineLearningPlatform.Domain.Utils;

public class PasswordHasher
{
    // Hash with salting: 
    public static string HashPassword(string plainText)
    {
        string salt = "Add just a little bit of salt!";
        byte[] saltBytes = Encoding.UTF8.GetBytes(salt);
        Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(plainText, saltBytes, 17, HashAlgorithmName.SHA512);
        byte[] hashBytes = rfc.GetBytes(64);
        string hashPassword = Convert.ToBase64String(hashBytes);
        return hashPassword;
    }
}
