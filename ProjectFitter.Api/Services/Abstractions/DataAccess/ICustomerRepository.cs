using ProjectFitter.Api.Data.Entities;

namespace ProjectFitter.Api.Services.Abstractions.DataAccess
{
    public interface ICustomerRepository
    {
        Task<bool> MobileNumberExists(string mobileNumber);
        Task<bool> EmailExists(string email);
        Task<bool> FullNameExists(string fullName);
        Task<bool> AddCustomer(Customer? customer);
        Task<bool> SixDigitsMatchByICNumber(string sixDigits, string icNumber);
    }
}
