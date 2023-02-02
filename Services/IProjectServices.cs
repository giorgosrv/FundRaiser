using FundRaiser.DTOs;
using FundRaiser.DTOs.Project;

namespace FundRaiser.Services
{
    public interface IProjectServices
    {
        public Task<GetProjectDto> GetProject(Guid id);
        public Task<List<GetProjectDto>> GetAllProjects();
        public Task<GetProjectDto> AddProject(CreateProjectDto dto);
        public Task<GetProjectDto> UpdateProject(Guid id, CreateProjectDto dto);
        public Task<bool> DeleteProject(Guid id);
        //public Task<List<GetBackerDto>> GetProjectBacker(Guid id);
    }
}
