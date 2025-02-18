using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectFitter.Api.Data;
using ProjectFitter.Api.Data.Entities;
using ProjectFitter.Api.Services.Abstractions.DataAccess;

namespace ProjectFitter.Api.Services.Implementations.DataAccess
{
    public class CustomerRepository(ApplicationDbContext dbContext, IUserService userService) : ICustomerRepository
    {
        public async Task<bool> MobileNumberExists(string mobileNumber)
        => await dbContext.Customers.AnyAsync(x => x.MobileNumber == mobileNumber);

        public async Task<bool> EmailExists(string email)
        => await dbContext.Customers.AnyAsync(x => x.EmailAddress == email);

        public async Task<bool> FullNameExists(string fullName)
        => await dbContext.Customers.AnyAsync(x => x.FullName == fullName);

        public async Task<bool> AddCustomer(Customer customer)
        {
            await dbContext.Customers.AddAsync(customer);
            return await dbContext.SaveChangesAsync() > 0;
        }

        public async Task UpdateCustomer(Customer customer)
        {
            dbContext.Customers.Update(customer);
            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> SixDigitsMatchByICNumber(string icNumber, string sixDigits)
        {
            var customer = await dbContext.Customers
                .Include(x => x.ICNumber)
                .FirstOrDefaultAsync(x => x.ICNumber.Number == icNumber);
            var match = userService.VerifyPassword(customer, sixDigits) == PasswordVerificationResult.Success;
            if (match)
            {
                customer.HasConfirmedSixDigitsPin = true;
                customer.IsDraft = false;
                await UpdateCustomer(customer);
            }
            return match;
        }
    }
}
