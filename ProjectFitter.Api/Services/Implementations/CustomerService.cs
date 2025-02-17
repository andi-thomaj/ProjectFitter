using ProjectFitter.Api.Controllers.Customer.Requests;
using ProjectFitter.Api.Data.Entities;
using ProjectFitter.Api.Helpers.ResultPattern;
using ProjectFitter.Api.Services.Abstractions;
using ProjectFitter.Api.Services.Abstractions.DataAccess;

namespace ProjectFitter.Api.Services.Implementations
{
    public class CustomerService(ICustomerRepository customerRepository, IICNumberRepository icNumberRepository) : ICustomerService
    {
        public async Task<Result> RegisterCustomerDraft(RegisterCustomerRequest request)
        {
            try
            {
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
                await customerRepository.AddCustomer(customer);

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
                await customerRepository.AddCustomer(customer);
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
                await customerRepository.AddCustomer(customer);
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
                customer.SixDigitsPin = request.SixDigitPin;
                await customerRepository.AddCustomer(customer);
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
                if (customer.SixDigitsPin != sixDigitPin)
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
                await customerRepository.AddCustomer(customer);
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

                customer.IsSMSVerified = false;
                customer.IsEmailVerified = false;
                customer.HasAcceptedTermsAndConditions = false;
                customer.SixDigitsPin = string.Empty;
                customer.HasEnabledSeamlessLogin = false;
                return new Result(true, Error.None);

            }
            catch (Exception e)
            {
                return new Result(false, Error.Failure(nameof(LoginWithICNumber), e.Message));
            }
        }
    }
}
