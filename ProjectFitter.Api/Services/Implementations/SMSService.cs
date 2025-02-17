using ProjectFitter.Api.Controllers.Customer.Requests;
using ProjectFitter.Api.Helpers.ResultPattern;
using ProjectFitter.Api.Services.Abstractions;
using ProjectFitter.Api.Services.Abstractions.DataAccess;

namespace ProjectFitter.Api.Services.Implementations
{
    public class SMSService(IICNumberRepository icNumberRepository) : ISMSService
    {
        public async Task<Result> SendSMS(SendSMSRequest request)
        {
            // Send SMS code
            try
            {
                return new Result(true, Error.None);
            }
            catch (Exception e)
            {
                return new Result(false, Error.Failure(nameof(SendSMS), e.Message));
            }
        }

        public async Task<Result> ValidateSMSCode(string smsCode)
        {
            // Validate SMS code
            try
            {
                var isValid = new Random().Next(2) == 0;

                if (!isValid)
                {
                    return new Result(false, Error.Problem(nameof(ValidateSMSCode), $"Invalid SMS code: {smsCode}"));
                }

                return new Result(true, Error.None);
            }
            catch (Exception e)
            {
                return new Result(false, Error.Failure(nameof(ValidateSMSCode), e.Message));
            }
        }
    }
}
