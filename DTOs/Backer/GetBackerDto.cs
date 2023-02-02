namespace FundRaiser.DTOs;

public class GetBackerDto: BaseEntityDto
{
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public List<string>? ProjectsFunded { get; set;}
}