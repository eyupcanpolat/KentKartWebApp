using KentKart.Api.Entities;
using Microsoft.AspNetCore.Identity;

namespace KentKart.Api.Helpers;

public static class PasswordHelper
{
    public static string HashPassword(User user, string password)
    {
        var passwordHasher = new PasswordHasher<User>();
        return passwordHasher.HashPassword(user, password);
    }

    public static bool VerifyPassword(User user, string password)
    {
        var passwordHasher = new PasswordHasher<User>();
        var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);

        return result == PasswordVerificationResult.Success;
    }
}