using JaMoveo.Models;
using Microsoft.AspNetCore.SignalR;

namespace JaMoveo.Hubs
{
    public class RehearsalHub : Hub
    {
        public async Task SelectSong(SongResult song)
        {
            await Clients.All.SendAsync("SongSelected", song);
        }

        public async Task QuitSession()
        {
            await Clients.All.SendAsync("SessionQuit");
        }
    }
}
