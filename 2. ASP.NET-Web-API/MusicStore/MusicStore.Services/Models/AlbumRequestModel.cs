namespace MusicStore.Services.Models
{
    using MusicStore.Models;
    using System.Collections.Generic;

    public class AlbumRequestModel
    {
        public string Title { get; set; }

        public int Year { get; set; }

        public string Producer { get; set; }

        public virtual ICollection<Artist> Artists { get; set; }

        public virtual ICollection<Song> Songs { get; set; }
    }
}