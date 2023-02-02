using Microsoft.AspNetCore.Mvc;
using FundRaiser.Exceptions;
using Microsoft.AspNetCore.Mvc;
using FundRaiser.DTOs;
using FundRaiser.Services;
using System.Data.SqlTypes;
using Microsoft.EntityFrameworkCore;
using FundRaiser.DTOs.Project;

namespace FundRaiser.Controllers
{
    public class RewardPackageController : ControllerBase
        
    {
        private IRewardPackageService _rewardPackageService;
        public RewardPackageController(IRewardPackageService rewardPackageService)
        {
            _rewardPackageService = rewardPackageService;
        }
        [HttpGet, Route("{id}")]
        public async Task<ActionResult<RewardPackageDto>>GetPackageById(Guid id)
        {
            var dto = await _rewardPackageService.GetPackageById(id);
            if (dto == null) return NotFound("Cannoo find RewardPackage with this id.");
            return Ok(dto);
        }

        [HttpDelete, Route("delete/{id}")]
        public async Task<ActionResult<bool>> DeletePackage(Guid id)
        {
            var deleted = await _rewardPackageService.DeletePackage(id);
                if (!deleted) return NotFound("The package you are trying to delete does not exist. ");
            return Ok(deleted);

        
        }
        [HttpPatch, Route("{packageId}")]
        public async Task<ActionResult<RewardPackageDto>> UpdatePackage([FromRoute] Guid packageId, [FromBody] RewardPackageDto dto)
        {
            var response = await _rewardPackageService.UpdatePackage(packageId, dto);
            if (response == null) return NotFound("The package you are trying to update does not exist.");
            return Ok(response);
        }
    }
}
