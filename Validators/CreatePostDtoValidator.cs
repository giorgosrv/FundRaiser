using FluentValidation;
using FundRaiser.DTOs;

namespace FundRaiser.Validators
{
    public class CreatePostDtoValidator : AbstractValidator<CreatePostDto>
    {
        public CreatePostDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Text).NotEmpty();
        }
    }
}
