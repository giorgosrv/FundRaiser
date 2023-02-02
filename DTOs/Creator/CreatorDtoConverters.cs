using FundRaiser.Models;

namespace FundRaiser.DTOs.Creator
{

    public static class CreatorDtoConverters
    {
        public static GetCreatorDto Convert(this Models.Creator creator)
        {
            return new GetCreatorDto()
            {
                Id = creator.Id,
                FirstName = creator.FirstName,
                LastName = creator.LastName,
                Email = creator.Email,
                CreatedAt = creator.CreatedAt,
                UpdatedAt = creator.UpdatedAt,
                Profession = creator.Profession,
                Projects = creator.Projects?.Select(p => p.Title).ToList(),
            };
        }

        public static Models.Creator Convert(this CreateCreatorDto creator)
        {
            return new Models.Creator()
            {
                Email = creator.Email!,
                FirstName = creator.FirstName!,
                LastName = creator.LastName!,
                Profession = creator.Profession!,
                UpdatedAt = DateTime.Now,
                IdentityId = creator.IdentityId!
            };
        }
    }
}
