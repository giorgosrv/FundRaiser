namespace FundRaiser.DTOs.Project
{
    public class GetProjectDto : BaseEntityDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int FinancialGoal { get; set; }
        public int CurrentAmount { get; set; }
        public string? Category { get; set; }
        public List<string>? Backers {get; set;}
        public string? CreatorId { get; set; }
    }
}
