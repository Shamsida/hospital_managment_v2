using hospital_management.Repository.Interface;
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
        [HttpGet("api/user/GetUsers")]
        public async Task<IActionResult> Get()
        {
            var itm = await patientService.Get();
            if (itm == null)
            {
                return BadRequest();
            }
            return Ok(itm);
        }
    }
}
