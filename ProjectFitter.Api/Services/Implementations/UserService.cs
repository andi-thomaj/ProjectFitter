using Microsoft.AspNetCore.Identity;
using ProjectFitter.Api.Data.Entities;

namespace ProjectFitter.Api.Services.Implementations
{
    public class UserService(IPasswordHasher<Customer> passwordHasher) : IUserService
    {
        public string HashPassword(Customer user, string password)
        {
            return passwordHasher.HashPassword(user, password);
        }

        public PasswordVerificationResult VerifyPassword(Customer user, string password)
        {
            return passwordHasher.VerifyHashedPassword(user, user.SixDigitsPin, password);
        }
    }

    public interface IUserService
    {
        string HashPassword(Customer user, string password);
        PasswordVerificationResult VerifyPassword(Customer user, string password);
    }
}
