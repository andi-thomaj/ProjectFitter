using ProjectFitter.Api.Controllers.Customer.Requests;
using ProjectFitter.Api.Helpers.ResultPattern;

namespace ProjectFitter.Api.Services.Abstractions
{
    public interface IEmailService
    {
        Task<Result> SendEmail(SendEmailRequest request);
        Task<Result> ValidateEmailCode(string emailCode);
    }
}
