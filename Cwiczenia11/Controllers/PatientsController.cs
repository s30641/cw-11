using Cwiczenia11.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cwiczenia11.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IDbService _dbService;
        
        public PatientsController(IDbService dbService)
        {
            _dbService = dbService;
        }
        
        [HttpGet("{idPatient}")]
        public async Task<IActionResult> GetPatientDetails(int idPatient)
        {
            try
            {
                var patientDetails = await _dbService.GetPatientDetails(idPatient);

                return Ok(patientDetails);
            }
            catch (Exception ex)
            {
                return NotFound(new 
                { 
                    error = ex.Message 
                });
            }
        }
    }
}