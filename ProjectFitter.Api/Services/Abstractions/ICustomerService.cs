using ProjectFitter.Api.Controllers.Customer.Requests;
using ProjectFitter.Api.Helpers.ResultPattern;

namespace ProjectFitter.Api.Services.Abstractions
{
    public interface ICustomerService
    {
        Task<Result> RegisterCustomerDraft(RegisterCustomerRequest request);
        Task<Result> ConfirmSMS(string icNumber);
        Task<Result> ConfirmEmail(string icNumber);
        Task<Result> HasAcceptedTermsAndConditions(string icNumber);
        Task<Result> CreateSixDigitPin(CreateSixDigitPinRequest request);
        Task<Result> ActivateBiometricLogin(string icNumber);
        Task<Result> LoginWithICNumber(string icNumber);
    }
}
