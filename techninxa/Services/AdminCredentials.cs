using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using techninxa.Models;
namespace Techninxa.Services;
public static class AdminCredentials
{
    private const string Prefix = "identity:";
    private static readonly PasswordHasher<AdminUser> Hasher = new();
    public static bool Verify(AdminUser user, string password)
    {
        if (string.IsNullOrWhiteSpace(password) || string.IsNullOrEmpty(user.Password) || !user.Password.StartsWith(Prefix, StringComparison.Ordinal)) return false;
        try { return Hasher.VerifyHashedPassword(user, user.Password[Prefix.Length..], password) != PasswordVerificationResult.Failed; }
        catch (FormatException) { return false; }
    }
    public static async Task BootstrapAsync(ApplicationDbContext db, IConfiguration config)
    {
        var email = config["BootstrapAdmin:Email"];
        var password = config["BootstrapAdmin:Password"];
        if (string.IsNullOrWhiteSpace(email) && string.IsNullOrWhiteSpace(password)) return;
        if (string.IsNullOrWhiteSpace(email) || email.Length > 150 || !new System.ComponentModel.DataAnnotations.EmailAddressAttribute().IsValid(email) || string.IsNullOrWhiteSpace(password) || password.Length < 14)
            throw new InvalidOperationException("Bootstrap requires a valid email and a password of at least 14 characters.");
        var user = await db.User.SingleOrDefaultAsync(x => x.Username == email);
        if (user == null) { user = new AdminUser { Username = email }; db.User.Add(user); }
        user.Password = Prefix + Hasher.HashPassword(user, password);
        await db.SaveChangesAsync();
    }
}
