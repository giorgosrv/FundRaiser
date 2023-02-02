namespace FundRaiser.Models;

public class Fund: BaseModel
{
    public Project Project { get; set; }
    public Guid ProjectId { get; set; }
    
    public Backer Backer { get; set; }
    public Guid BackerId { get; set; }
    
    public  int Amount {get; set; }
    
    public RewardPackage RewardPackage { get; set; }
    public Guid RewardPackageId { get; set; }
}