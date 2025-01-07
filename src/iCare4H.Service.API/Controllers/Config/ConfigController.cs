using iCare4H.Service.Domain.Interface;
using iCare4H.Service.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace iCare4H.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ConfigController(IConfigService configService) : ControllerBase
    {
        private readonly IConfigService _configService = configService;

        [AllowAnonymous]
        [HttpGet("admin/ping")]
        public IActionResult PingRequest()
        {
            return Ok("Ping worked!!!");
        }

        [AllowAnonymous]
        [HttpGet("admin/getdata")]
        public IActionResult GetAllUserType([FromQuery] string name)
        {
            var result = _configService.GetAdminMasterJsonData(name);

            if (result is null || result.AdminMasterId <= 0)
                return BadRequest(new { message = "Failure!!!" });

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("admin/add")]
        public IActionResult AddNewAdminData([FromBody] CfgAdminMaster adminMaster)
        {
            var result = _configService.AddNewMasterData(adminMaster);

            if (!result)
                return BadRequest(new { message = "Failure!!!" });

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("admin/sync")]
        public IActionResult SyncMasterData([FromBody] CfgAdminMaster adminMaster)
        {
            var result = _configService.SyncMasterData(adminMaster);

            if (!result)
                return BadRequest(new { message = "Failure!!!" });

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("admin/delete")]
        public IActionResult DeleteMasterData([FromQuery] int id)
        {
            var result = _configService.DeleteMasterData(id);

            if (!result)
                return BadRequest(new { message = "Failure!!!" });

            return Ok(result);
        }
    }
}