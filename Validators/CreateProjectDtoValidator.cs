using FluentValidation;
using FundRaiser.DTOs.Project;

namespace FundRaiser.Validators
{
    public class CreateProjectDtoValidator : AbstractValidator<CreateProjectDto>
    {
        public CreateProjectDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Category).NotEmpty();
            RuleFor(x => x.FinancialGoal > 0);
            RuleFor(x => x.CreatorId).NotEmpty();
        }
        
    }
}
