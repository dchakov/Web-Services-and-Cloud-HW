namespace MusicStore.Services.Models
{
    using MusicStore.Models;
    using System;
    using System.Collections.Generic;

    public class ArtistRequestModel
    {
        public string Name { get; set; }

        public string Country { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public virtual ICollection<Album> Albums { get; set; }
    }
}