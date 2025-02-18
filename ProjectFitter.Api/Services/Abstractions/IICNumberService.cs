using ProjectFitter.Api.Controllers.Customer.Responses;
using ProjectFitter.Api.Helpers.ResultPattern;

namespace ProjectFitter.Api.Services.Abstractions
{
    public interface IICNumberService
    {
        Task<Result<GetCustomerByICNumberResponse>> GetCustomerByICNumber(string icNumber);
    }
}
