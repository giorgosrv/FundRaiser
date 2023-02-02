using FundRaiser.Data;
using FundRaiser.DTOs;
using FundRaiser.Models;
using Microsoft.EntityFrameworkCore;

namespace FundRaiser.Services
{
    public class RewardPackageService : IRewardPackageService
    {
        private readonly FundRaiserContext _context;
        public RewardPackageService(FundRaiserContext context)
        {
            _context = context;
        }
        public async Task<bool> DeletePackage(Guid id)
        {
            RewardPackage rewardPackage = await _context.RewardPackages
                .SingleOrDefaultAsync(p => p.Id == id);
            if(rewardPackage==null)return false;
            _context.Remove(rewardPackage);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<RewardPackageDto> GetPackageById(Guid id)
        {
            RewardPackage rewardPackage = await _context.RewardPackages.FindAsync(id);
            var dto = new RewardPackageDto()
            {
                Title = rewardPackage.Title,
                Description = rewardPackage.Description,
                NeedToFund = rewardPackage.NeedToFund,
            };
            return dto;
            //throw new NotImplementedException();
        }

        public async Task<RewardPackageDto> UpdatePackage(Guid packageId, RewardPackageDto dto)
        {
            RewardPackage rewardPackage = await _context.RewardPackages
                .SingleOrDefaultAsync(p => p.Id == packageId);
            if (rewardPackage is null) return null;

            if (dto.Title is not null) rewardPackage.Title = dto.Title;
            if (dto.Description is not null) rewardPackage.Description = dto.Description;
            if(dto.NeedToFund !=0)rewardPackage.NeedToFund = dto.NeedToFund;
            await _context.SaveChangesAsync();
            RewardPackageDto new_dto = new()
            {
                Title = rewardPackage.Title,
                Description = rewardPackage.Description,
                NeedToFund = rewardPackage.NeedToFund,
            };
            return new_dto;
        }
    }
}
