using FundRaiser.DTOs;


namespace FundRaiser.Services
{
    public interface IRewardPackageService
    {
        public Task<RewardPackageDto> GetPackageById(Guid id);
        public Task<bool> DeletePackage(Guid id);
        public Task<RewardPackageDto> UpdatePackage(Guid packageId, RewardPackageDto dto);
    }
}
