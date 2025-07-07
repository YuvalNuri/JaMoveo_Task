using JaMoveo.DATA;
using JaMoveo.Models;
using Microsoft.EntityFrameworkCore;

namespace JaMoveo.DB
{
    public class DBInstruments
    {
        private readonly AppDbContext _context;

        public DBInstruments(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InstrumentItem>> GetAllAsync()
    => await _context.Instruments
                 .OrderBy(i => i.Name)
                 .ToListAsync();

        public async Task<InstrumentItem> GetOrCreateInstrumentAsync(string rawName)
        {
            var name = rawName.Trim().ToLowerInvariant();

            var instrument = await _context.Instruments.FindAsync(name);

            if (instrument is null) //not exist - insert
            {
                instrument = new InstrumentItem { Name = name };
                _context.Instruments.Add(instrument);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException) //2 same inserts at the same time
                {
                    instrument = await _context.Instruments.FindAsync(name);
                }
            }

            return instrument;
        }
    }
}
