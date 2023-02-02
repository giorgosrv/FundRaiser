using FundRaiser.Data;
using FundRaiser.DTOs;
using FundRaiser.DTOs.Creator;
using FundRaiser.Exceptions;
using FundRaiser.Models;
using FundRaiser.Validators;
using Microsoft.EntityFrameworkCore;

namespace FundRaiser.Services
{
    public class CreatorService : ICreatorService
    {
        private readonly FundRaiserContext _context;
        public CreatorService(FundRaiserContext context)
        {
            _context = context;
        }

        public async Task<GetCreatorDto> GetCreator(Guid id)
        {
            var creator = await _context.Creators!
                .Include(c => c.Projects)
                .SingleOrDefaultAsync(b => b.Id.Equals(id));
            if (creator == null) throw new NotFoundException("Creator not found.");
            return creator.Convert();
        }
        public async Task<List<GetCreatorDto>> GetCreators()
        {
            return await _context.Creators!
                .Include(b => b.Projects)
                .Select(b => b.Convert())
                .ToListAsync();
        }
        public async Task<GetCreatorDto> AddCreator(CreateCreatorDto creator)
        {
            var creatorExists = await _context
                .Backers!
                .SingleOrDefaultAsync(b => b.Email.Equals(creator.Email));
            if (creatorExists != null)
                throw new AlreadyExistException("Creator with email : " + creator.Email + " already exists ");

            var validator = new CreateCreatorDtoValidator();
            if (validator.Validate(creator).IsValid is false)
                throw new ArgumentNullException("Creator must have a valid Email, LastName, FirstName and Profession");

            var createdCreator = await _context.Creators!.AddAsync(creator.Convert());
            await _context.SaveChangesAsync();

            var temp = createdCreator.Entity;
            return temp.Convert();
        }
        public async Task<bool> DeleteCreator(Guid id)
        {
            var creatorExists = await _context
                .Creators!
                .SingleOrDefaultAsync(b => b.Id.Equals(id));
            if (creatorExists == null)
            {
                throw new NotFoundException("Creator not found.");
            }
            _context.Creators!.Remove(creatorExists);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<GetCreatorDto> UpdateCreator(Guid id, CreateCreatorDto updatedCreator)
        {
            var existingCreator = await _context.Creators!
                .SingleOrDefaultAsync(b => b.Id == id);
            if (existingCreator == null) throw new NotFoundException("Creator not found.");

            if (updatedCreator.Email is not null && updatedCreator.Email != existingCreator.Email)
            {
                var existingMail = await _context.Creators!.SingleOrDefaultAsync(b => b.Email == updatedCreator.Email);
                if (existingMail is not null) throw new AlreadyExistException("Creator with email : " + updatedCreator.Email + " already exists ");
                existingCreator.Email = updatedCreator.Email;
            }
            if (updatedCreator.FirstName is not null) existingCreator.FirstName = updatedCreator.FirstName;
            if (updatedCreator.LastName is not null) existingCreator.LastName = updatedCreator.LastName;

            existingCreator.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return existingCreator.Convert();
        }
    }
}
