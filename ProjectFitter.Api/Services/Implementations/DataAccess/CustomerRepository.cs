using Microsoft.EntityFrameworkCore;
using ProjectFitter.Api.Data;
using ProjectFitter.Api.Data.Entities;
using ProjectFitter.Api.Services.Abstractions.DataAccess;

namespace ProjectFitter.Api.Services.Implementations.DataAccess
{
    public class CustomerRepository(ApplicationDbContext dbContext) : ICustomerRepository
    {
        public async Task<bool> MobileNumberExists(string mobileNumber)
        => await dbContext.Customers.AnyAsync(x => x.MobileNumber == mobileNumber);

        public async Task<bool> EmailExists(string email)
        => await dbContext.Customers.AnyAsync(x => x.EmailAddress == email);

        public async Task<bool> FullNameExists(string fullName)
        => await dbContext.Customers.AnyAsync(x => x.FullName == fullName);

        public async Task<bool> AddCustomer(Customer? customer)
        {
            await dbContext.Customers.AddAsync(customer);
            return await dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> SixDigitsMatchByICNumber(string sixDigits, string icNumber)
        => await dbContext.Customers
                .Include(x => x!.ICNumber)
                .AnyAsync(x => x.ICNumber.Number == icNumber && x.SixDigitsPin == sixDigits);
    }
}
