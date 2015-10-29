namespace MusicStore.Data
{
    using Models;
    using System.Data.Entity;

    public class MusicStoreDbContext : DbContext
    {
        public MusicStoreDbContext()
            : base("MusicStoreEntities")
        {
        }

        public virtual IDbSet<Artist> Artists { get; set; }

        public virtual IDbSet<Song> Songs { get; set; }

        public virtual IDbSet<Album> Albums { get; set; }

    }
}
