using ProjectFitter.Api.Controllers.Customer.Requests;
using ProjectFitter.Api.Helpers.ResultPattern;
using ProjectFitter.Api.Services.Abstractions.DataAccess;

namespace ProjectFitter.Api.Services.Implementations
{
    public class EmailService(IICNumberRepository icNumberRepository)
    {
        public async Task<Result> SendEmail(SendEmailRequest request)
        {
            // Send Email code
            try
            {
                return new Result(true, Error.None);
            }
            catch (Exception e)
            {
                return new Result(false, Error.Failure(nameof(SendEmail), e.Message));
            }
        }

        public async Task<Result> ValidateEmailCode(string emailCode)
        {
            // Validate Email code
            try
            {
                var isValid = new Random().Next(2) == 0;

                if (!isValid)
                {
                    return new Result(false, Error.Problem(nameof(ValidateEmailCode), $"Invalid Email code: {emailCode}"));
                }

                return new Result(true, Error.None);
            }
            catch (Exception e)
            {
                return new Result(false, Error.Failure(nameof(ValidateEmailCode), e.Message));
            }
        }
    }
}
