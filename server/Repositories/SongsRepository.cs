using JaMoveo.Models;
using System.Text.Json;

namespace JaMoveo.Repositories
{
    public class SongsRepository
    {
        public List<SongResult> SearchSongs(string query)
        {
            var folder = Path.Combine(Directory.GetCurrentDirectory(), "Files");
            var files = Directory.GetFiles(folder, "*.json");

            var matches = new List<SongResult>();
            var q = query?.Trim().ToLower();

            foreach (var file in files)
            {
                var filename = Path.GetFileName(file); // include extension
                var filenameWithoutExtension = Path.GetFileNameWithoutExtension(file);
                var songNameWithSpaces = filenameWithoutExtension.Replace("_", " ");

                // song name contains
                if (!string.IsNullOrEmpty(q) && songNameWithSpaces.ToLower().Contains(q))
                {
                    matches.Add(new SongResult
                    {
                        Name = songNameWithSpaces,
                        FileName = filename,
                        Artist = "no artist info",
                        Img = null
                    });
                    continue;
                }

                // lyrics contains
                var json = File.ReadAllText(file);
                var lyrics = System.Text.Json.JsonSerializer.Deserialize<List<List<LyricEntry>>>(json);

                if (lyrics != null)
                {
                    if (lyrics.Any(line => line.Any(word =>
                        !string.IsNullOrEmpty(word.lyrics) &&
                        word.lyrics.Contains(query, StringComparison.OrdinalIgnoreCase))))
                    {
                        matches.Add(new SongResult
                        {
                            Name = songNameWithSpaces,
                            FileName = filename,
                            Artist = "no artist info",
                            Img = null
                        });
                    }
                }
            }

            return matches;
        }

        public object? GetSongContent(string fileName)
        {
            var folder = Path.Combine(Directory.GetCurrentDirectory(), "Files");
            var filePath = Path.Combine(folder, fileName);

            if (!File.Exists(filePath))
                return null;

            var json = File.ReadAllText(filePath);

            var data = JsonSerializer.Deserialize<object>(json);
            return data;
        }
    }
}
