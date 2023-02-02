using FundRaiser.Data;
using FundRaiser.DTOs.Fund;
using FundRaiser.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FundRaiser.Services
{
    public class FundService : IFundService
    {
        private readonly FundRaiserContext _context;

        public FundService(FundRaiserContext context)
        {
            _context = context;
        }

        public async Task<List<GetFundDto>> GetAllFunds()
        {
            return await _context.Funds
                .Select(f => f.Convert())
                .ToListAsync();
        }

        public async Task<GetFundDto> GetFund(Guid id)
        {
            var fund = await _context.Funds
                .SingleOrDefaultAsync(f => f.Id == id);
            if (fund == null) throw new NotFoundException ("Subscription not found.");
            return fund.Convert();
        }

        public async Task<GetFundDto> AddFund(CreateFundDto dto)
        {
            if (dto.ProjectId == Guid.Empty || dto.BackerId == Guid.Empty || dto.RewardPackageId == Guid.Empty)
            {
                throw new ArgumentException("You must provide a valid project id, backer id and reward package id");
            }
            
            var backer = await _context.Backers
                .Include(b => b.ProjectsFund)
                .SingleOrDefaultAsync(b => b.Id == dto.BackerId);
            if (backer is null) throw new NotFoundException("Backer not found.");

            var project = await _context.Projects
                .Include(p => p.BackersFund)
                .SingleOrDefaultAsync(p => p.Id == dto.ProjectId);
            if (project is null) throw new NotFoundException("Project not found.");

            var package = await _context.RewardPackages.SingleOrDefaultAsync(p => p.Id == dto.RewardPackageId);
            if (package is null) throw new NotFoundException("Reward packahe not found.");

            if (package.ProjectId != dto.ProjectId)
            {
                throw new ArgumentException("Project and Reward Package do not match.");
            }

            if (dto.Amount < package.NeedToFund)
            {
                throw new ArgumentException("Fund amount must be equal or greater than: " + package.NeedToFund.ToString());
            }

            backer.ProjectsFund.Add(project);
            project.BackersFund.Add(backer);
            project.CurrentFund += dto.Amount;

            var newFund = dto.Convert();
            newFund.Project = project;
            newFund.Backer = backer;
            newFund.RewardPackage = package;

            var retFund = await _context.Funds.AddAsync(newFund);
            await _context.SaveChangesAsync();

            return retFund.Entity.Convert();

        }
    }
}
