using FundRaiser.Exceptions;
using Microsoft.AspNetCore.Mvc;
using FundRaiser.DTOs;
using FundRaiser.Services;
using System.Data.SqlTypes;
using Microsoft.EntityFrameworkCore;
using FundRaiser.DTOs.Project;

namespace FundRaiser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BackersController : ControllerBase
    {
        private IBackerService _backerService;

        public BackersController(IBackerService backerService)
        {
            _backerService = backerService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetBackerDto>>> Get()
        {
            var backer =await _backerService.GetBackers();
            return Ok(backer);
        }
        
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GetBackerDto>> Get(Guid id)
        {
            try
            {
                var backer =await _backerService.GetBacker(id);
                return Ok(backer);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<GetBackerDto>> AddBacker(CreateBackerDto backer)
        {

            try
            {
                var retBacker = await _backerService.AddBacker(backer);
                return Ok(retBacker);
            }
            catch (AlreadyExistException e)
            {
                return StatusCode(409, e.Message);
            }
            catch (ArgumentNullException e)
            {
                return BadRequest(e.Message);
            }
        }

        /*
        [HttpPost, Route("Fund")]
        public async Task<ActionResult<GetProjectDto>> FundProject(FundDto fund)
        {
            try
            {
                var fundtemp = await _backerService.FundProject(fund.backerId, fund.projectId, fund.amount);
                return Ok(fund);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }
        */

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteBacker(Guid id)
        {
            try
            {
                await _backerService.DeleteBacker(id);
                return Ok("Backer deleted successfully.");
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPatch, Route("{id}")]
        public async Task<ActionResult> UpdateBacker(Guid id, CreateBackerDto backer)
        {
            try
            {
                var updatedBacker = await _backerService.UpdateBacker(id, backer);
                return Ok(updatedBacker);
            }
            catch (AlreadyExistException e)
            {
                return StatusCode(409, e.Message);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
