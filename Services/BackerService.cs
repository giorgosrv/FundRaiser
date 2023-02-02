using FundRaiser.Data;
using Microsoft.EntityFrameworkCore;
using FundRaiser.DTOs;
using FundRaiser.Exceptions;
using FundRaiser.Data;
using FundRaiser.Models;
using FundRaiser.DTOs;
using FundRaiser.Services;
using FundRaiser.DTOs.Project;
using FundRaiser.Validators;

namespace FundRaiser.Services
{
    public class BackerService : IBackerService
    {
        private readonly FundRaiserContext _context;
        public BackerService(FundRaiserContext context) 
        { 
            _context = context;
        }
        public async Task<GetBackerDto> AddBacker(CreateBackerDto backer)
        {
            var backerExists = await _context
                .Backers!
                .SingleOrDefaultAsync(b => b.Email.Equals(backer.Email));
            if (backerExists != null)
                throw new AlreadyExistException("Backer with email : " + backer.Email + " already exists ");

            var validator = new CreateBackerDtoValidator();
            if (validator.Validate(backer).IsValid is false)
                throw new ArgumentNullException("Backer must have a valid Email, LastName, and FirstName");

            var createdBacker = await _context.Backers!.AddAsync(backer.Convert());
            await _context.SaveChangesAsync();

            var temp = createdBacker.Entity;
            return temp.Convert();
        }
        
        public async Task<bool> DeleteBacker(Guid id)
        {
            var backerExists = await _context
                .Backers!
                .SingleOrDefaultAsync(b => b.Id.Equals(id));
            if (backerExists == null)
            {
                throw new NotFoundException("Backer not found.");
            }
            _context.Backers!.Remove(backerExists);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<GetBackerDto> GetBacker(Guid id)
        {
            var backer = await _context.Backers!
                .Include(b => b.ProjectsFund)
                .SingleOrDefaultAsync(b => b.Id.Equals(id));
            if (backer == null) throw new NotFoundException("Backer not found.");
            return backer.Convert();
        }
        
        public async Task<List<GetBackerDto>> GetBackers()
        {
            return await _context.Backers!
                .Include(b => b.ProjectsFund)
                .Select(b => b.Convert())
                .ToListAsync();
        }

        public async Task<GetBackerDto> UpdateBacker(Guid id, CreateBackerDto updatedBacker)
        {
            var existingBacker = await _context.Backers!
                .SingleOrDefaultAsync(b => b.Id == id);
            if (existingBacker == null) throw new NotFoundException("Backer not found.");

            if (updatedBacker.Email is not null && updatedBacker.Email != existingBacker.Email)
            {
                var existingMail = await _context.Backers!.SingleOrDefaultAsync(b => b.Email== updatedBacker.Email);
                if (existingMail is not null) throw new AlreadyExistException("Backer with email : " + updatedBacker.Email + " already exists ");
                existingBacker.Email = updatedBacker.Email;
            }
            if (updatedBacker.FirstName is not null) existingBacker.FirstName = updatedBacker.FirstName;
            if (updatedBacker.LastName is not null) existingBacker.LastName = updatedBacker.LastName;

            existingBacker.UpdatetAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return existingBacker.Convert();

        }

        /*
        public async Task<GetProjectDto> FundProject(Guid backerId, Guid projectId, int amount)
        {
            var existingBacker = await _context.Backers
                .Include(b => b.ProjectsFund)
                .SingleOrDefaultAsync(b => b.Id == backerId);

            if (existingBacker == null) throw new NotFoundException("Backer not found.");

            var existingProject = await _context.Projects
                .Include(p => p.BackersFund)
                .SingleOrDefaultAsync(p => p.Id == projectId);

            if (existingProject == null) throw new NotFoundException("Project not found.");
            if (amount <= 0) throw new ArgumentException("Fund amount must be positive.");

            existingProject.CurrentFund += amount;
            existingProject.BackersFund.Add(existingBacker);
            existingBacker.ProjectsFund.Add(existingProject);
            await _context.SaveChangesAsync();

            return existingProject.Convert();
        }
        */
    }
}
