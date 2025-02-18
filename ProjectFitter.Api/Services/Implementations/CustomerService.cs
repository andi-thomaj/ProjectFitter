using Microsoft.AspNetCore.Identity;
using ProjectFitter.Api.Controllers.Customer.Requests;
using ProjectFitter.Api.Controllers.Customer.Responses;
using ProjectFitter.Api.Data.Entities;
using ProjectFitter.Api.Helpers.ResultPattern;
using ProjectFitter.Api.Services.Abstractions;
using ProjectFitter.Api.Services.Abstractions.DataAccess;

namespace ProjectFitter.Api.Services.Implementations
{
    public class CustomerService(
        ICustomerRepository customerRepository
        , IICNumberRepository icNumberRepository
        , IUserService userService) : ICustomerService
    {
        public async Task<Result> RegisterCustomerDraft(RegisterCustomerRequest request)
        {
            try
            {
                var icNumberExists = await icNumberRepository.ICNumberExists(request.ICNumber);
                if (icNumberExists)
                {
                    return new Result(false, Error.Conflict(nameof(RegisterCustomerDraft), "IC number already exists"));
                }

                var icNumber = new ICNumber
                {
                    Number = request.ICNumber
                };

                var customer = new Customer
                {
                    FullName = request.FullName,
                    EmailAddress = request.EmailAddress,
                    MobileNumber = request.MobileNumber,
                    ICNumber = new ICNumber
                    {
                        Number = request.ICNumber
                    },
                    IsDraft = true
                };

                await customerRepository.AddCustomer(customer);

                return new Result(true, Error.None);
            }
            catch (Exception e)
            {
                return new Result(false, Error.Failure(nameof(RegisterCustomerDraft), e.Message));
            }
        }

        public async Task<Result> ConfirmSMS(string icNumber)
        {
            try
            {
                var customer = await icNumberRepository.GetCustomerByICNumber(icNumber);
                if (customer == null)
                {
                    return new Result(false, Error.NotFound(nameof(ConfirmSMS), "Customer not found"));
                }
                customer.IsSMSVerified = true;
                await customerRepository.UpdateCustomer(customer);

                return new Result(true, Error.None);
            }
            catch (Exception e)
            {
                return new Result(false, Error.Failure(nameof(ConfirmSMS), e.Message));
            }
        }

        public async Task<Result> ConfirmEmail(string icNumber)
        {
            try
            {
                var customer = await icNumberRepository.GetCustomerByICNumber(icNumber);
                if (customer == null)
                {
                    return new Result(false, Error.NotFound(nameof(ConfirmEmail), "Customer not found"));
                }
                customer.IsEmailVerified = true;
                await customerRepository.UpdateCustomer(customer);
                return new Result(true, Error.None);
            }
            catch (Exception e)
            {
                return new Result(false, Error.Failure(nameof(ConfirmEmail), e.Message));
            }
        }

        public async Task<Result> HasAcceptedTermsAndConditions(string icNumber)
        {
            try
            {
                var customer = await icNumberRepository.GetCustomerByICNumber(icNumber);
                if (customer == null)
                {
                    return new Result(false, Error.NotFound(nameof(HasAcceptedTermsAndConditions), "Customer not found"));
                }
                customer.HasAcceptedTermsAndConditions = true;
                await customerRepository.UpdateCustomer(customer);
                return new Result(true, Error.None);
            }
            catch (Exception e)
            {
                return new Result(false, Error.Failure(nameof(HasAcceptedTermsAndConditions), e.Message));
            }
        }

        public async Task<Result> CreateSixDigitPin(CreateSixDigitPinRequest request)
        {
            try
            {
                var customer = await icNumberRepository.GetCustomerByICNumber(request.ICNumber);
                if (customer == null)
                {
                    return new Result(false, Error.NotFound(nameof(CreateSixDigitPin), "Customer not found"));
                }
                customer.SixDigitsPin = userService.HashPassword(customer, request.SixDigitPin);
                await customerRepository.UpdateCustomer(customer);
                return new Result(true, Error.None);
            }
            catch (Exception e)
            {
                return new Result(false, Error.Failure(nameof(CreateSixDigitPin), e.Message));
            }
        }

        public async Task<Result> ValidateSixDigitPin(string icNumber, string sixDigitPin)
        {
            try
            {
                var customer = await icNumberRepository.GetCustomerByICNumber(icNumber);
                if (customer == null)
                {
                    return new Result(false, Error.NotFound(nameof(ValidateSixDigitPin), "Customer not found"));
                }
                if (userService.VerifyPassword(customer, sixDigitPin) == PasswordVerificationResult.Success)
                {
                    return new Result(false, Error.Problem(nameof(ValidateSixDigitPin), "Invalid six digit pin"));
                }
                return new Result(true, Error.None);
            }
            catch (Exception e)
            {
                return new Result(false, Error.Failure(nameof(ValidateSixDigitPin), e.Message));
            }
        }

        public async Task<Result> ActivateBiometricLogin(string icNumber)
        {
            try
            {
                var customer = await icNumberRepository.GetCustomerByICNumber(icNumber);
                if (customer == null)
                {
                    return new Result(false, Error.NotFound(nameof(ActivateBiometricLogin), "Customer not found"));
                }
                customer.HasEnabledSeamlessLogin = true;
                await customerRepository.UpdateCustomer(customer);
                return new Result(true, Error.None);
            }
            catch (Exception e)
            {
                return new Result(false, Error.Failure(nameof(ActivateBiometricLogin), e.Message));
            }
        }

        public async Task<Result> LoginWithICNumber(string icNumber)
        {
            try
            {
                var customer = await icNumberRepository.GetCustomerByICNumber(icNumber);
                if (customer == null)
                {
                    return new Result(false, Error.NotFound(nameof(LoginWithICNumber), "Customer not found"));
                }

                customer.IsDraft = true;
                customer.IsSMSVerified = false;
                customer.IsEmailVerified = false;
                customer.HasAcceptedTermsAndConditions = false;
                customer.SixDigitsPin = null;
                customer.HasEnabledSeamlessLogin = false;
                customer.HasConfirmedSixDigitsPin = false;
                await customerRepository.UpdateCustomer(customer);
                return new Result(true, Error.None);

            }
            catch (Exception e)
            {
                return new Result(false, Error.Failure(nameof(LoginWithICNumber), e.Message));
            }
        }

        public async Task<Result<GetCustomerByICNumberResponse>> GetCustomerByICNumber(string icNumber)
        {
            try
            {
                var customer = await icNumberRepository.GetCustomerByICNumber(icNumber);
                if (customer == null)
                {
                    return new Result<GetCustomerByICNumberResponse>(null, false,
                        Error.NotFound(nameof(GetCustomerByICNumber),
                            $"Customer with IC number: {icNumber} not found"));
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
                return new Result<GetCustomerByICNumberResponse>(null, false,
                    Error.Failure(nameof(GetCustomerByICNumber), e.Message));
            }
        }
    }
}
