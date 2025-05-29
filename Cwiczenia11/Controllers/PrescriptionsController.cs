using Cwiczenia11.DTOs;
using Cwiczenia11.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cwiczenia11.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrescriptionsController : ControllerBase
    {
        private readonly IDbService _dbService;
        
        public PrescriptionsController(IDbService dbService)
        {
            _dbService = dbService;
        }
        
        [HttpPost]
        public async Task<IActionResult> AddPrescription([FromBody] CreatePrescriptionRequestDto request)
        {
            try
            {
                var idPrescription = await _dbService.AddPrescription(request);

                return Ok(new 
                { 
                    IdPrescription = idPrescription 
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new 
                { 
                    error = ex.Message 
                });
            }
        }
    }
}