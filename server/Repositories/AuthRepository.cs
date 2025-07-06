using JaMoveo.DB;
using JaMoveo.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace JaMoveo.Repositories
{
    public class AuthRepository
    {
        private readonly DBAuth _dbAuth;
        public AuthRepository(DBAuth dbAuth) => _dbAuth = dbAuth;
        public async Task<(bool success, string error)> RegisterAsync(RegisterRequest registerRequest)
        {
            if (await _dbAuth.UsernameExistsAsync(registerRequest.Username)) //check username availability
                return (false, "Username already taken");

            var instrument =
       await _dbAuth.GetOrCreateInstrumentAsync(registerRequest.Instrument.Name); //get/ create instrument if not exist

            using var hmac = new HMACSHA512(); //create automatic salt
            var user = new User
            {
                Username = registerRequest.Username,
                PasswordSalt = hmac.Key, //save salt
                PasswordHash = hmac.ComputeHash( //calculate Hash
                    Encoding.UTF8.GetBytes(registerRequest.Password)),
                Instrument = instrument,
                IsAdmin = false,
            };

            return await _dbAuth.RegisterAsync(user);
        }

        public async Task<List<object>> GetAllUsers()
        {
            var users = await _dbAuth.GetAllUsers();
            var dto = users.Select(u => (object)new
            {
                Username = u.Username,
                Instrument = u.Instrument?.Name,
                IsAdmin = u.IsAdmin
            }).ToList();
            return dto;
        }

        public async Task<List<InstrumentItem>> GetAllInstruments()
        {
            return await _dbAuth.GetAllInstruments();
        }
    }
}
