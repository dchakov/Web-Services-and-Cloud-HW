namespace MusicStore.Services.Controllers
{
    using Data.Repositories;
    using Models;
    using MusicStore.Models;
    using System.Web.Http;

    public class SongController : ApiController
    {
        private IGenericRepository<Song> repository;

        public SongController()
        {
            this.repository = new GenericRepository<Song>();
        }

        public SongController(IGenericRepository<Song> repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            return this.Ok(this.repository.SelectAll());
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return this.Ok(this.repository.SelectById(id));
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]SongRequestModel song)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var newSong = new Song
            {
                Title = song.Title,
                Year = song.Year,
                Genre = song.Genre,
                ArtistId = song.ArtistId,
                AlbumId = song.AlbumId
            };

            this.repository.Insert(newSong);
            this.repository.SaveChanges();

            return this.Ok(newSong);
        }

        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]SongRequestModel song)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var currentSong = this.repository.SelectById(id);

            if (currentSong == null)
            {
                return this.BadRequest("Invalid Id");
            }

            currentSong.Title = string.IsNullOrEmpty(song.Title) ? currentSong.Title : song.Title;
            currentSong.Year = song.Year;
            currentSong.Genre = string.IsNullOrEmpty(song.Genre) ? currentSong.Genre : song.Genre;
            currentSong.ArtistId = song.ArtistId;
            currentSong.AlbumId = song.AlbumId;

            this.repository.Update(currentSong);
            this.repository.SaveChanges();
            return this.Ok(currentSong);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var song = this.repository.SelectById(id);

            if (song == null)
            {
                return this.BadRequest("Invalid id");
            }

            this.repository.Delete(id);
            this.repository.SaveChanges();
            return this.Ok();
        }
    }
}