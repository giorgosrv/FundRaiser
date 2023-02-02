namespace FundRaiser.DTOs.Fund
{
    public class GetFundDto : BaseEntityDto
    {
        public Guid BackerId { get; set; }
        public Guid ProjectId { get; set; }
        public Guid RewardPackageId { get; set; }
        public int Amount { get; set; }
    }
}
