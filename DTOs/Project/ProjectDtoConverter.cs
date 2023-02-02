using FundRaiser.Models;

namespace FundRaiser.DTOs.Project
{
    public static class ProjectDtoConverter
    {
        public static GetProjectDto Convert(this Models.Project project)
        {
            return new GetProjectDto()
            {
                Id = project.Id,
                Title= project.Title,
                FinancialGoal= project.FinancialGoal,
                CurrentAmount = project.CurrentFund ?? default(int),
                Description= project.Description,
                CreatedAt = project.CreatedAt,
                UpdatedAt = project.UpdatetAt,
                Category = project.Category,
                Backers = project.BackersFund?.Select(b => b.Email).ToList(),
                CreatorId = project.CreatorId.ToString()
            };
        }

        public static Models.Project Convert(this CreateProjectDto project)
        {
            return new Models.Project()
            {
                Title = project.Title!,
                Description = project.Description!,
                FinancialGoal = project.FinancialGoal ?? default(int),
                UpdatetAt = DateTime.Now,
                Category = project.Category!,
                CreatorId = project.CreatorId
            };
        }
    }
}
