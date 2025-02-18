using ProjectFitter.Api.Controllers.Customer.Responses;
using ProjectFitter.Api.Helpers.ResultPattern;
using ProjectFitter.Api.Services.Abstractions;
using ProjectFitter.Api.Services.Abstractions.DataAccess;

namespace ProjectFitter.Api.Services.Implementations
{
    public class ICNumberService(IICNumberRepository icNumberRepository) : IICNumberService
    {
        public async Task<Result<GetCustomerByICNumberResponse>> GetCustomerByICNumber(string icNumber)
        {
            try
            {
                var customer = await icNumberRepository.GetCustomerByICNumber(icNumber);
                if (customer == null)
                {
                    return new Result<GetCustomerByICNumberResponse>(null, false, Error.NotFound(nameof(GetCustomerByICNumber), $"Customer with IC number: {icNumber} not found"));
                }

                var response = new GetCustomerByICNumberResponse
                {
                    FullName = customer.FullName,
                    EmailAddress = customer.EmailAddress,
                    MobileNumber = customer.MobileNumber,
                    ICNumber = customer.ICNumber.Number,
                    IsSMSVerified = customer.IsSMSVerified,
                    IsEmailVerified = customer.IsEmailVerified,
                    HasAcceptedTermsAndConditions = customer.HasAcceptedTermsAndConditions,
                    HasConfirmedSixDigitsPin = customer.HasConfirmedSixDigitsPin,
                    HasEnabledSeamlessLogin = customer.HasEnabledSeamlessLogin,
                    IsDraft = customer.IsDraft
                };

                return new Result<GetCustomerByICNumberResponse>(response, true, Error.None);
            }
            catch (Exception e)
            {
                return new Result<GetCustomerByICNumberResponse>(null,false, Error.Failure(nameof(GetCustomerByICNumber), e.Message));
            }
        }
    }
}
