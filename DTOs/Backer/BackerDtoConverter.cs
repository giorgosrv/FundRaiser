using  FundRaiser.Models;

namespace FundRaiser.DTOs
{

    public static class BackerDtoConverters
    {
        public static GetBackerDto Convert(this Backer backer)
        {
            return new GetBackerDto()
            {
                Id = backer.Id,
                FirstName = backer.FirstName,
                LastName = backer.LastName,
                Email = backer.Email,
                CreatedAt = backer.CreatedAt,
                UpdatedAt = backer.UpdatetAt,
                ProjectsFunded = backer.ProjectsFund?.Select(p => p.Title).ToList(),
            };
        }
        
        public static Backer Convert(this CreateBackerDto backer)
        {
            return new Backer()
            {
                Email = backer.Email!,
                FirstName = backer.FirstName!,
                LastName = backer.LastName!,
                UpdatetAt= DateTime.Now,
            };
        }
    }
}