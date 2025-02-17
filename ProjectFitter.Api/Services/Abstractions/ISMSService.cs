using ProjectFitter.Api.Controllers.Customer.Requests;
using ProjectFitter.Api.Helpers.ResultPattern;

namespace ProjectFitter.Api.Services.Abstractions
{
    public interface ISMSService
    {
        Task<Result> SendSMS(SendSMSRequest request);
        Task<Result> ValidateSMSCode(string smsCode);
    }
}
