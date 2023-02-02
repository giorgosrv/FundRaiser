using FundRaiser.Exceptions;
using Microsoft.AspNetCore.Mvc;
using FundRaiser.DTOs;
using FundRaiser.Services;
using System.Data.SqlTypes;
using Microsoft.EntityFrameworkCore;
using FundRaiser.DTOs.Project;
using FundRaiser.DTOs.Creator;

namespace FundRaiser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreatorController : ControllerBase
    {
        private ICreatorService _creatorService;

        public CreatorController(ICreatorService creatorService)
        {
            _creatorService = creatorService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetCreatorDto>>> Get()
        {
            var creator =await _creatorService.GetCreators();
            return Ok(creator);
        }
        
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GetCreatorDto>> Get(Guid id)
        {
            try
            {
                var creator =await _creatorService.GetCreator(id);
                return Ok(creator);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CreateCreatorDto>> AddCreator(CreateCreatorDto creator)
        {

            try
            {
                var retCreator = await _creatorService.AddCreator(creator);
                return Ok(retCreator);
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


        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteCreator(Guid id)
        {
            try
            {
                await _creatorService.DeleteCreator(id);
                return Ok("Creator deleted successfully.");
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPatch, Route("{id}")]
        public async Task<ActionResult> UpdateCreator(Guid id, CreateCreatorDto creator)
        {
            try
            {
                var updatedCreator = await _creatorService.UpdateCreator(id, creator);
                return Ok(updatedCreator);
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
