using JaMoveo.DATA;
using JaMoveo.DB;
using JaMoveo.Models;
using JaMoveo.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JaMoveo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthRepository _Auth;
        public AuthController(AuthRepository Auth) => _Auth = Auth;
        //// GET: api/<AuthController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<AuthController>/5
        [HttpGet("AllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            //try
            //{
            //    var users = await _Auth.GetAllUsers();
            //    if(users.Count() > 0)
            //    {
            //        return Ok(users);
            //    }
            //    return Ok();
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest("failed to read users");
            //}

        }

        [HttpGet("AllInstruments")]
        public async Task<IActionResult> GetAllInstruments()
        {
            try
            {
                var instruments = await _Auth.GetAllInstruments();
                if (instruments.Count() > 0)
                {
                    return Ok(instruments);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("failed to read users");
            }
        }

        // POST api/<AuthController>
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            var success = await _Auth.RegisterAsync(registerRequest);
            if (success.success)
            {
                return Ok(true);
            }
            else
                return BadRequest("Couldn't register user");
        }

        //// PUT api/<AuthController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<AuthController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
