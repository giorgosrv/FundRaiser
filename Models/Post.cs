namespace FundRaiser.Models;

public class Post : BaseModel
{
    public string Title { get; set; }
    public string? Text { get; set; }
    public Project? Project { get; set; }
}