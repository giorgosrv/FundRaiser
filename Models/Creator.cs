using FundRaiser.DTOs;

namespace FundRaiser.Models;

public class Creator: BaseEntityDto
{
    public string IdentityId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Profession { get; set; }
    public string Email { get; set; }
    
    public List<Project>? Projects { get; set; }
}