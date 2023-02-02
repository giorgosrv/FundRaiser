using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FundRaiser.Models;

public class RewardPackage : BaseModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int NeedToFund { get; set; }
    public int ReturnAmount { get; set; }
    public Project? Project { get; set; }
    public Guid? ProjectId { get; set; }
    
    public List<Fund> Funds { get; set; }

    public RewardPackage()
    {
        Funds = new List<Fund>();
    }
}