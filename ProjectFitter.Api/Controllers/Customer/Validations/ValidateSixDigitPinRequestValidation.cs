using FluentValidation;
using ProjectFitter.Api.Controllers.Customer.Requests;
using ProjectFitter.Api.Services.Abstractions.DataAccess;

namespace ProjectFitter.Api.Controllers.Customer.Validations
{
    public class ValidateSixDigitPinRequestValidation : AbstractValidator<ValidateSixDigitPinRequest>
    {
        private readonly IICNumberRepository _icNumberRepository;
        private readonly ICustomerRepository _customerRepository;

        public ValidateSixDigitPinRequestValidation(IICNumberRepository icNumberRepository, ICustomerRepository customerRepository)
        {
            _icNumberRepository = icNumberRepository;
            _customerRepository = customerRepository;
            _icNumberRepository = icNumberRepository;

            RuleFor(x => x.SixDigitPin)
                .NotNull()
                .WithMessage("Six digit pin is required")
                .NotEmpty()
                .WithMessage("Six digit pin is required")
                .Matches(@"^\d{6}$")
                .WithMessage("Six digit pin is invalid.")
                .MustAsync(async (request, ignore, cancellationToken) => await SixDigitPinExists(request.ICNumber, request.SixDigitPin, CancellationToken.None))
                .WithMessage("fsa");

            RuleFor(x => x.ICNumber)
                .NotEmpty()
                .WithMessage("IC number is required")
                .Matches(@"^\d{12}$")
                .WithMessage("IC number must be exactly 12 digits long and contain only numbers.")
                .MustAsync(ICNumberExists)
                .WithMessage("IC Number doesn't exist");
        }

        private async Task<bool> ICNumberExists(string number, CancellationToken cancellationToken)
        {
            return await _icNumberRepository.ICNumberExists(number);
        }

        private async Task<bool> SixDigitPinExists(string number, string sixDigits, CancellationToken cancellationToken)
        {
            return await _customerRepository.SixDigitsMatchByICNumber(number, sixDigits);
        }
    }
}
