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
        private readonly DBInstruments _dbInst;

        public AuthRepository(DBAuth dbAuth, DBInstruments dbInstruments)
        {
            _dbAuth = dbAuth;
            _dbInst = dbInstruments;
        }
        public async Task<(bool success, string error)> RegisterAsync(RegisterRequest registerRequest)
        {
            if (await _dbAuth.UsernameExistsAsync(registerRequest.Username)) //check username availability
                return (false, "Username already taken");

            var instrument =
       await _dbInst.GetOrCreateInstrumentAsync(registerRequest.Instrument); //get/ create instrument if not exist

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

        public async Task<(bool success, string error, object? user)> LoginAsync(LoginRequest loginRequest)
        {
            var user = await _dbAuth.GetUserByUsernameAsync(loginRequest.Username);

            if (user == null)
                return (false, "User not found", null);

            using var hmac = new HMACSHA512(user.PasswordSalt);  //the saved salt

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginRequest.Password)); //compute hash of inserted password by the saved salt

            if (!CryptographicOperations.FixedTimeEquals(computedHash, user.PasswordHash)) // compare
                return (false, "Invalid password", null);

            return (true, "",new
            {
                Username = user.Username,
                Instrument = user.Instrument?.Name,
                IsAdmin = user.IsAdmin
            });
        }

    }
}
