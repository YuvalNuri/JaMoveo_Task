using JaMoveo.DB;
using JaMoveo.Models;
using System.Security.Cryptography;
using System.Text;

namespace JaMoveo.Repositories
{
    public class InstrumentsRepository
    {
        private readonly DBInstruments _db;
        public InstrumentsRepository(DBInstruments db) => _db = db;

        public async Task<IEnumerable<InstrumentItem>> GetAllAsync()
        {
            return await _db.GetAllAsync();
        }
    }
}
