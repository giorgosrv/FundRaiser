using FundRaiser.Models;

namespace FundRaiser.DTOs.Fund
{
    public static class FundDtoConverter
    {
        public static GetFundDto Convert(this Models.Fund fund)
        {
            return new GetFundDto
            {
                Id = fund.Id,
                BackerId = fund.BackerId,
                ProjectId = fund.ProjectId,
                RewardPackageId = fund.RewardPackageId,
                Amount= fund.Amount,
                CreatedAt = fund.CreatedAt,
                UpdatedAt = fund.UpdatetAt
            };
        }

        public static Models.Fund Convert(this CreateFundDto fund)
        {
            return new Models.Fund
            {
                BackerId = fund.BackerId,
                ProjectId = fund.ProjectId,
                RewardPackageId = fund.RewardPackageId,
                Amount = fund.Amount,
            };
        }
    }
}
