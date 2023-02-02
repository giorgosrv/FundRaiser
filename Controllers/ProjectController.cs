using FundRaiser.DTOs;
using FundRaiser.DTOs.Project;
using FundRaiser.Exceptions;
using FundRaiser.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FundRaiser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private IProjectServices _projectService;
        public ProjectController(IProjectServices projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetProjectDto>>> Get()
        {
            var backer = await _projectService.GetAllProjects();
            return Ok(backer);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GetProjectDto>> Get(Guid id)
        {
            try
            {
                var project = await _projectService.GetProject(id);
                return Ok(project);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CreateProjectDto>> AddProject(CreateProjectDto project)
        {

            try
            {
                var newproject = await _projectService.AddProject(project);
                return Ok(newproject);
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
        public async Task<ActionResult> DeleteProject(Guid id)
        {
            try
            {
                await _projectService.DeleteProject(id);
                return Ok("Project deleted successfully.");
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPatch, Route("{id}")]
        public async Task<ActionResult> UpdateProject(Guid id, CreateProjectDto projectChanges)
        {
            try
            {
                var updatedProject = await _projectService.UpdateProject(id, projectChanges);
                return Ok(updatedProject);
            }
            catch (AlreadyExistException e)
            {
                return StatusCode(409, e.Message);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e);
            }
        }

        /*
        [HttpGet, Route("Backers/{id}")]
        public async Task<ActionResult<List<GetBackerDto>>> GetProjectBackers(Guid id)
        {
            try
            {
                return Ok(await _projectService.GetProjectBacker(id));
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
        */
    }
}
