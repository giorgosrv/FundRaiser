using System.ComponentModel.DataAnnotations;

namespace FundRaiser.Models;

public class Backer : BaseModel
{

    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    
    public List<Project>? ProjectsFund { get; set; }

    public int? ReturnedAmount { get; set; } = 0;

}