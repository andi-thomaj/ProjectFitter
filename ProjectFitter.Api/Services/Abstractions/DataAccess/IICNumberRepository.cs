using ProjectFitter.Api.Data.Entities;

namespace ProjectFitter.Api.Services.Abstractions.DataAccess
{
    public interface IICNumberRepository
    {
        Task<bool> ICNumberExists(string number);
        Task<Customer?> GetCustomerByICNumber(string icNumber);
    }
}
