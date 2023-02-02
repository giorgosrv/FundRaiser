using FluentValidation;
using FundRaiser.DTOs;

namespace FundRaiser.Validators
{
    public class CreateBackerDtoValidator : AbstractValidator<CreateBackerDto>
    {
        public CreateBackerDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
        }
    }
}
