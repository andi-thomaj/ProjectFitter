using Microsoft.EntityFrameworkCore;
using ProjectFitter.Api.Data;
using ProjectFitter.Api.Data.Entities;
using ProjectFitter.Api.Services.Abstractions.DataAccess;

namespace ProjectFitter.Api.Services.Implementations.DataAccess
{
    public class ICNumberRepository(ApplicationDbContext dbContext) : IICNumberRepository
    {
        public async Task<bool> ICNumberExists(string number)
         => await dbContext.ICNumbers.AnyAsync(x => x.Number == number);

        public async Task<Customer?> GetCustomerByICNumber(string icNumber)
        {
            return await dbContext.Customers
                .Include(x => x!.ICNumber)
                .FirstOrDefaultAsync(x => x.ICNumber.Number == icNumber);
        }
    }
}
