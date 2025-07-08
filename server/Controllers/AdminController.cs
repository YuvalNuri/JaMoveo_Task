using JaMoveo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JaMoveo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly SongsRepository _songrepo;
        public AdminController(SongsRepository songrepo) => _songrepo = songrepo;

        [HttpGet("songs")]
        public IActionResult GetSongs([FromQuery] string query)
        {
            var results = _songrepo.SearchSongs(query);
            return Ok(results);
        }

        [HttpGet("song")]
        public IActionResult GetSongJson([FromQuery] string fileName)
        {
            var song = _songrepo.GetSongContent(fileName);
            if (song == null)
                return NotFound();
            return Ok(song);
        }
    }
}
