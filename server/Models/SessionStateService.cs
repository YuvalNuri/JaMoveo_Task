namespace JaMoveo.Models
{
    public class SessionStateService
    {
        public bool IsActive { get; set; } = false;
        public SongResult? CurrentSong { get; set; }
    }
}
