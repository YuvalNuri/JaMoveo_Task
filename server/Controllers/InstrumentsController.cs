using JaMoveo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

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
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Debug/Tables")]
        public IActionResult GetTables()
        {
            using var connection = new SqliteConnection("Data Source=app.db");
            connection.Open();

            var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table'";

            var tables = new List<string>();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                tables.Add(reader.GetString(0));
            }

            return Ok(tables);
        }

    }
}
