using FundRaiser.Data;
using FundRaiser.DTOs;
using FundRaiser.DTOs.Project;
using FundRaiser.Exceptions;
using FundRaiser.Validators;
using Microsoft.EntityFrameworkCore;

namespace FundRaiser.Services
{
    public class ProjectServices : IProjectServices
    {
        private readonly FundRaiserContext _context;
        public ProjectServices(FundRaiserContext context)
        {
            _context = context;
        }

        public async Task<GetProjectDto> AddProject(CreateProjectDto dto)
        {
            var validator= new CreateProjectDtoValidator();

            if (validator.Validate(dto).IsValid is false)
            {
                throw new ArgumentNullException("A project must hava a valid title, description, category, financial goal and creator id.");
            }
            var projectExists = await _context.Projects.SingleOrDefaultAsync(p => p.Title.Equals(dto.Title));
            if (projectExists is not null) throw new AlreadyExistException("Project with this title already exists.");

            var newProject = dto.Convert();
            await _context.Projects.AddAsync(newProject);
            await _context.SaveChangesAsync();
            return newProject.Convert();

        }

        public async Task<bool> DeleteProject(Guid id)
        {
            var projectFound = await _context.Projects.SingleOrDefaultAsync(p => p.Id == id);
            if (projectFound is null) throw new NotFoundException("Project not found.");

            _context.Projects.Remove(projectFound);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<GetProjectDto>> GetAllProjects()
        {
            return await _context.Projects
                .Select(p => p.Convert())
                .ToListAsync();
        }

        public async Task<GetProjectDto> GetProject(Guid id)
        {
            var project = await _context.Projects!
                .SingleOrDefaultAsync(p => p.Id.Equals(id));
            if (project == null) throw new NotFoundException("Project not found.");
            return ProjectDtoConverter.Convert(project);
        }

        /*
        public async Task<List<GetBackerDto>> GetProjectBacker(Guid id)
        {
            var projectFound = await _context.Projects
                .Include(p => p.BackersFund)
                .SingleOrDefaultAsync(p => p.Id == id);
            if (projectFound is null) throw new NotFoundException("Project not found.");
            return projectFound.BackersFund
                .Select(b => b.Convert())
                .ToList();
        }
        */

        public async Task<GetProjectDto> UpdateProject(Guid id, CreateProjectDto dto)
        {
            var existingProject = await _context.Projects!
                .SingleOrDefaultAsync(p => p.Id == id);
            if (existingProject is null) throw new NotFoundException("Project not found.");

            if (dto.Title is not null)
            {
                var existingTitle = await _context.Projects!.SingleOrDefaultAsync(p => p.Title == dto.Title);
                if (existingTitle is not null) throw new AlreadyExistException("Project with title : " + dto.Title + " already exists ");
                existingProject.Title = dto.Title;
            }

            if (dto.Description is not null) existingProject.Description = dto.Description;
            if (dto.Category is not null) existingProject.Category = dto.Category;

            if (dto.FinancialGoal is not null && dto.FinancialGoal < 0)
                throw new ArgumentException("Financial amount must be positive.");
            else
                existingProject.FinancialGoal = dto.FinancialGoal ?? default(int);

            existingProject.UpdatetAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return existingProject.Convert();
        }
    }
}
