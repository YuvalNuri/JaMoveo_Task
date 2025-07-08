using Microsoft.AspNetCore.SignalR;

namespace JaMoveo.Hubs
{
    public class RehearsalHub : Hub
    {
        public async Task SelectSong(string songName)
        {
            await Clients.All.SendAsync("SongSelected", songName);
        }

        public async Task QuitSession()
        {
            await Clients.All.SendAsync("SessionQuit");
        }
    }
}
