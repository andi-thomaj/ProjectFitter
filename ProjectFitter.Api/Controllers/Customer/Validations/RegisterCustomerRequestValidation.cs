using FluentValidation;
using ProjectFitter.Api.Controllers.Customer.Requests;
using ProjectFitter.Api.Services.Abstractions.DataAccess;

namespace ProjectFitter.Api.Controllers.Customer.Validations
{
    public class RegisterCustomerRequestValidation : AbstractValidator<RegisterCustomerRequest>
    {
        private readonly IICNumberRepository _icNumberRepository;

        public RegisterCustomerRequestValidation(IICNumberRepository icNumberRepository)
        {
            _icNumberRepository = icNumberRepository;

            RuleFor(x => x.ICNumber)
                .NotEmpty()
                .WithMessage("IC number is required")
                .Matches(@"^\d{12}$")
                .WithMessage("IC number must be exactly 12 digits long and contain only numbers.")
                .MustAsync(ICNumberExists)
                .WithMessage("IC Number already exists");

            RuleFor(x => x.MobileNumber)
                .NotEmpty()
                .WithMessage("Mobile number is required")
                .Matches(@"^\+60\d{9}$")
                .WithMessage("Phone number must start with '+60' and contain exactly 9 digits afterwards.");

            RuleFor(x => x.EmailAddress)
                .NotEmpty()
                .WithMessage("Email address is required")
                .EmailAddress()
                .WithMessage("Email address is not valid");

            RuleFor(x => x.FullName)
                .NotEmpty()
                .WithMessage("Full name is required")
                .MaximumLength(100)
                .WithMessage("Full name must be less than 100 characters long");
        }

        private async Task<bool> ICNumberExists(string number, CancellationToken cancellationToken)
        {
            return !await _icNumberRepository.ICNumberExists(number);
        }
    }
}
