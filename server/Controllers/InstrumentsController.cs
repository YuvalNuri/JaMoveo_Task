using JaMoveo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JaMoveo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstrumentsController : ControllerBase
    {
        private readonly InstrumentsRepository _Inst;
        public InstrumentsController(InstrumentsRepository Inst) => _Inst = Inst;

        [HttpGet("AllInstruments")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var instruments = await _Inst.GetAllAsync();
                if (instruments.Count() > 0)
                {
                    return Ok(instruments);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("failed to read instruments");
            }
        }
    }
}
