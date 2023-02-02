using FundRaiser.DTOs;
using FundRaiser.DTOs.Project;

namespace FundRaiser.Services
{
    public interface IBackerService
    {
        public Task<GetBackerDto> GetBacker(Guid id);
        public Task<List<GetBackerDto>> GetBackers();
        public Task<GetBackerDto> AddBacker(CreateBackerDto backer);
        public Task<bool> DeleteBacker(Guid id);
        public Task<GetBackerDto> UpdateBacker(Guid id, CreateBackerDto backer);
        //public Task<GetProjectDto> FundProject(Guid backerId, Guid projectId, int amount);
    }
}
