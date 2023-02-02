using FluentValidation;
using FundRaiser.DTOs;
using FundRaiser.DTOs.Creator;

namespace FundRaiser.Validators
{
    public class CreateCreatorDtoValidator : AbstractValidator<CreateCreatorDto>
    {
        public CreateCreatorDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Profession).NotEmpty();
        }
    }
}
