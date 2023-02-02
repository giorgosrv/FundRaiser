using System.ComponentModel.DataAnnotations;

namespace FundRaiser.Models;

public class Project : BaseModel
{
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public int FinancialGoal { get; set; }
    public int? CurrentFund { get; set; } = 0;
    [Required]
    public string Category { get; set; }

    public List<Backer>? BackersFund { get; set; }
    public List<Post>? Posts { get; set; }
    public Guid? CreatorId { get; set; }
    public Creator? Creator { get; set; }
    
    public List<RewardPackage>? RewardPackages { get; set; }

    public Project()
    {
        Posts = new List<Post>();
        BackersFund = new List<Backer>();
        RewardPackages = new List<RewardPackage>();

    }
}