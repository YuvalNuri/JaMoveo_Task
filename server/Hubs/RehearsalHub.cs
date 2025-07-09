using JaMoveo.Models;
using Microsoft.AspNetCore.SignalR;

namespace JaMoveo.Hubs
{
    public class RehearsalHub : Hub
    {
        private readonly SessionStateService _session;

        public RehearsalHub(SessionStateService session)
        {
            _session = session;
        }
        public async Task SelectSong(SongResult song)
        {
            await Clients.All.SendAsync("SongSelected", song);
        }

        public async Task QuitSession()
        {
            await Clients.All.SendAsync("SessionQuit");
        }

        public Task<SongResult?> GetCurrentSongIfActive()
        {
            if (_session.IsActive && _session.CurrentSong != null)
            {
                return Task.FromResult<SongResult?>(_session.CurrentSong);
            }

            return Task.FromResult<SongResult?>(null);
        }

    }
}
