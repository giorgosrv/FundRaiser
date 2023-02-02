namespace FundRaiser.DTOs.Fund
{
    public class CreateFundDto
    {
        public Guid BackerId { get; set; }
        public Guid ProjectId { get; set; } 
        public Guid RewardPackageId { get; set; }
        public int Amount { get; set; }
    }
}
