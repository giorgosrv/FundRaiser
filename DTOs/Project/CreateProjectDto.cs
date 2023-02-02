namespace FundRaiser.DTOs.Project
{
    public class CreateProjectDto
    {
        public Guid? CreatorId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? FinancialGoal { get; set; }
        public string? Category { get; set; }

    }
}
