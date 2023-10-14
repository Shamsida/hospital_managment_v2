//using hospital_management.service.Repository.Interface;
using hospital_management.BLL.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hospital_management.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService patientService;

        public PatientController(IPatientService patientService)
        {
            this.patientService = patientService;
        }

        //[Authorize(Roles = "admin")]
        [HttpGet("api/patients/GetPatients")]
        public async Task<IActionResult> Get()
        {
            var itm = await patientService.Get();
            if (itm == null)
            {
                return BadRequest();
            }
            return Ok(itm);
        }

        [HttpGet("api/patient/GetPatientById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var itm = await patientService.GetById(id);
            if (itm == null)
            {
                return NotFound();
            }
            return Ok(itm);
        }

        [HttpGet("api/patient/GetPatientByUsername")]
        public async Task<IActionResult> GetByUsername(string username)
        {
            var itm = await patientService.GetByUsername(username);
            if (itm == null)
            {
                return NotFound();
            }
            return Ok(itm);
        }

    }
}
