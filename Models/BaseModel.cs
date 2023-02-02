namespace FundRaiser.Models;

public class BaseModel
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatetAt { get; set; }
}