using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ProjectFitter.Api.Controllers.Customer.Requests;
using ProjectFitter.Api.Services.Abstractions;

namespace ProjectFitter.Api.Controllers.Customer
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController(
        ICustomerService customerService
        , ISMSService smsService
        , IEmailService emailService
        , IValidator<RegisterCustomerRequest> registerCustomerRequestValidator
        , IValidator<ValidateSMSCodeRequest> validateSMSCodeRequestValidator
        , IValidator<SendSMSRequest> sendSMSRequestValidator
        , IValidator<SendEmailRequest> sendEmailRequestValidator
        , IValidator<ValidateEmailCodeRequest> validateEmailCodeRequestValidator
        , IValidator<AcceptTermsAndConditionsRequest> acceptTermsAndConditionsRequestValidator
        , IValidator<CreateSixDigitPinRequest> createSixDigitPinRequestValidator
        , IValidator<ValidateSixDigitPinRequest> validateSixDigitPinRequestValidator
        , IValidator<ActivateBiometricLoginRequest> activateBiometricLoginRequestValidator
        , IValidator<LoginWithICNumberRequest> loginWithICNumberRequestValidation
        , IValidator<GetCustomerByICNumberRequest> getCustomerByICNumberRequestValidator
        ) : ControllerBase
    {
        [HttpPost(nameof(CreateDraftCustomer))]
        public async Task<ActionResult> CreateDraftCustomer([FromBody] RegisterCustomerRequest request)
        {
            var validationResult = await registerCustomerRequestValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result = await customerService.RegisterCustomerDraft(request);

            if (result.IsFailure)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost(nameof(SendSMS))]
        public async Task<ActionResult> SendSMS([FromBody] SendSMSRequest request)
        {
            var validationResult = await sendSMSRequestValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var sendSMSResult = await smsService.SendSMS(request);
            if (sendSMSResult.IsFailure)
            {
                return BadRequest(sendSMSResult.Error.Description);
            }

            return Ok();
        }

        [HttpPost(nameof(ValidateSMSCode))]
        public async Task<ActionResult> ValidateSMSCode([FromBody] ValidateSMSCodeRequest request)
        {
            var validationResult = await validateSMSCodeRequestValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var validateSMSCodeResult= await smsService.ValidateSMSCode(request.SMSCode);
            if (validateSMSCodeResult.IsFailure)
            {
                return BadRequest(validateSMSCodeResult.Error.Description);
            }

            var confirmSMSResult = await customerService.ConfirmSMS(request.ICNumber);
            if (confirmSMSResult.IsFailure)
            {
                return BadRequest(confirmSMSResult.Error.Description);
            }

            return Ok();
        }

        [HttpPost(nameof(SendEmail))]
        public async Task<ActionResult> SendEmail([FromBody] SendEmailRequest request)
        {
            var validationResult = await sendEmailRequestValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var sendEmailResult = await emailService.SendEmail(request);
            if (sendEmailResult.IsFailure)
            {
                return BadRequest(sendEmailResult.Error.Description);
            }

            return Ok();
        }

        [HttpPost(nameof(ValidateEmailCode))]
        public async Task<ActionResult> ValidateEmailCode([FromBody] ValidateEmailCodeRequest request)
        {
            var validationResult = await validateEmailCodeRequestValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var validateEmailCodeResult = await emailService.ValidateEmailCode(request.EmailCode);
            if (validateEmailCodeResult.IsFailure)
            {
                return BadRequest(validateEmailCodeResult.Error.Description);
            }

            var confirmEmailResult = await customerService.ConfirmEmail(request.ICNumber);
            if (confirmEmailResult.IsFailure)
            {
                return BadRequest(confirmEmailResult.Error.Description);
            }

            return Ok();
        }

        [HttpGet(nameof(AcceptTermsAndConditions))]
        public async Task<ActionResult> AcceptTermsAndConditions([FromQuery] string icNumber)
        {
            var validationResult = await acceptTermsAndConditionsRequestValidator.ValidateAsync(new AcceptTermsAndConditionsRequest { ICNumber = icNumber });
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result = await customerService.HasAcceptedTermsAndConditions(icNumber);
            if (result.IsFailure)
            {
                return BadRequest(result.Error.Description);
            }

            return Ok();
        }

        [HttpPost(nameof(CreateSixDigitPin))]
        public async Task<ActionResult> CreateSixDigitPin([FromBody] CreateSixDigitPinRequest request)
        {
            var validationResult = await createSixDigitPinRequestValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result = await customerService.CreateSixDigitPin(request);
            if (result.IsFailure)
            {
                return BadRequest(result.Error.Description);
            }

            return Ok();
        }

        [HttpPost(nameof(ValidateSixDigitPin))]
        public async Task<ActionResult> ValidateSixDigitPin([FromBody] ValidateSixDigitPinRequest request)
        {
            var validationResult = await validateSixDigitPinRequestValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            return Ok();
        }

        [HttpGet(nameof(ActivateBiometricLogin))]
        public async Task<ActionResult> ActivateBiometricLogin([FromQuery] string icNumber)
        {
            var validationResult = await activateBiometricLoginRequestValidator.ValidateAsync(new ActivateBiometricLoginRequest { ICNumber = icNumber });
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result = await customerService.ActivateBiometricLogin(icNumber);
            if (result.IsFailure)
            {
                return BadRequest(result.Error.Description);
            }

            return Ok();
        }

        [HttpGet(nameof(LoginWithICNumber))]
        public async Task<ActionResult> LoginWithICNumber([FromQuery] string icNumber)
        {
            var validationResult = await loginWithICNumberRequestValidation.ValidateAsync(new LoginWithICNumberRequest { ICNumber = icNumber });
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result = await customerService.LoginWithICNumber(icNumber);
            if (result.IsFailure)
            {
                return BadRequest(result.Error.Description);
            }

            return Ok();
        }

        [HttpGet(nameof(GetCustomerByICNumber))]
        public async Task<ActionResult> GetCustomerByICNumber([FromQuery] string icNumber)
        {
            var validationResult = await getCustomerByICNumberRequestValidator.ValidateAsync(new GetCustomerByICNumberRequest { ICNumber = icNumber });
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result = await customerService.GetCustomerByICNumber(icNumber);
            if (result.IsFailure)
            {
                return BadRequest(result.Error.Description);
            }
            return Ok(result.Value);
        }
    }
}
