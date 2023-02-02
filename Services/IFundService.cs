using FundRaiser.DTOs.Fund;

namespace FundRaiser.Services
{
    public interface IFundService
    {
        public Task<List<GetFundDto>> GetAllFunds();
        public Task<GetFundDto> GetFund(Guid id);
        public Task<GetFundDto> AddFund(CreateFundDto dto);
    }
}
