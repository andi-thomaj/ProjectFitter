using FluentValidation;
using ProjectFitter.Api.Controllers.Customer.Requests;
using ProjectFitter.Api.Services.Abstractions.DataAccess;

namespace ProjectFitter.Api.Controllers.Customer.Validations
{
    public class ValidateSMSCodeRequestValidation : AbstractValidator<ValidateSMSCodeRequest>
    {
        private readonly IICNumberRepository _icNumberRepository;

        public ValidateSMSCodeRequestValidation(IICNumberRepository icNumberRepository)
        {
            _icNumberRepository = icNumberRepository;
            RuleFor(x => x.SMSCode)
                .NotNull()
                .WithMessage("SMS code is required")
                .NotEmpty()
                .WithMessage("SMS code is required")
                .Matches(@"^\d{4}$")
                .WithMessage("SMS code is invalid.");

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
    }
}
