using FundRaiser.DTOs;
using FundRaiser.DTOs.Creator;

namespace FundRaiser.Services
{
    public interface ICreatorService
    {
        public Task<GetCreatorDto> GetCreator(Guid id);
        public Task<List<GetCreatorDto>> GetCreators();
        public Task<GetCreatorDto> AddCreator(CreateCreatorDto creator);
        public Task<bool> DeleteCreator(Guid id);
        public Task<GetCreatorDto> UpdateCreator(Guid id, CreateCreatorDto backer);
    }
}
