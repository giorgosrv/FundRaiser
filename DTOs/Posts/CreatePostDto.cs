namespace FundRaiser.DTOs
{
    public class CreatePostDto 
    {
        
        public string? Title { get; set; }
        public string? Text { get; set; }
        public Guid? ProjectId { get; set; }
    }
}
