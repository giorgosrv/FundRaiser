namespace FundRaiser.DTOs
{
    public class GetPostDto : BaseEntityDto
    {
        
        public string Title { get; set; }
        public string Text { get; set; }
        public string ProjectId { get; set; }
    }
}
