namespace MusicStore.Services.Models
{
    public class SongRequestModel
    {
        public string Title { get; set; }

        public int Year { get; set; }

        public string Genre { get; set; }

        public int ArtistId { get; set; }

        public int AlbumId { get; set; }
    }
}