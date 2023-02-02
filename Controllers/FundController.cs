using FundRaiser.DTOs.Fund;
using FundRaiser.Services;
using Microsoft.AspNetCore.Mvc;

namespace FundRaiser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FundController : ControllerBase
    {
        private IFundService _fundService;

        public FundController(IFundService fundService)
        {
            _fundService = fundService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetFundDto>>> Get()
        {
            var funds = await _fundService.GetAllFunds();
            return Ok(funds);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GetFundDto>> Get(Guid id)
        {
            try
            {
                var fund = await _fundService.GetFund(id);
                return Ok(fund);
            }
            catch (NotFiniteNumberException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<GetFundDto>> AddFund(CreateFundDto dto)
        {
            try
            {
                var retFund = await _fundService.AddFund(dto);
                return Ok(retFund);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (FundRaiser.Exceptions.NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
