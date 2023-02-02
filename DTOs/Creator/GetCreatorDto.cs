namespace FundRaiser.DTOs.Creator
{
    public class GetCreatorDto : BaseEntityDto
    {
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Profession { get; set; }
        public List<string>? Projects { get; set; }
    }
}
