using JaMoveo.DATA;
using JaMoveo.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Security.Cryptography;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JaMoveo.DB
{
    public class DBAuth
    {
        private readonly AppDbContext _context;

        public DBAuth(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(bool success, string error)> RegisterAsync(User user)
        {
            _context.Users.Add(user); //add to DbContext
            try
            {
                var rows = await _context.SaveChangesAsync(); // insert & save
                if (rows > 0)
                    return (true, string.Empty);
                else
                    return (false, "Couldn't register user");
            }
            catch (DbUpdateException ex)
            {
                return (false, "Database error while creating user");
            }
        }

        public async Task<bool> UsernameExistsAsync(string username)
    => await _context.Users.AnyAsync(u => u.Username == username);

        public async Task<InstrumentItem> GetOrCreateInstrumentAsync(string instrumentName)
        {
            var instrument = await _context.Instruments
                                           .SingleOrDefaultAsync(i => i.Name == instrumentName);

            if (instrument is null)
            {
                instrument = new InstrumentItem { Name = instrumentName };
                _context.Instruments.Add(instrument);
                await _context.SaveChangesAsync();
            }
            return instrument;
        }


        public async Task<List<User>> GetAllUsers() => await _context.Users.ToListAsync();

        public async Task<List<InstrumentItem>> GetAllInstruments() => await _context.Instruments.ToListAsync();


    }
}
