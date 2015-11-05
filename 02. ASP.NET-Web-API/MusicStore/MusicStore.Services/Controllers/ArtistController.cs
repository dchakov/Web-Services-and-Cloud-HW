namespace MusicStore.Services.Controllers
{
    using Data.Repositories;
    using Models;
    using MusicStore.Models;
    using System.Web.Http;

    public class ArtistController : ApiController
    {
        private IGenericRepository<Artist> repository;

        public ArtistController()
        {
            this.repository = new GenericRepository<Artist>();
        }

        public ArtistController(IGenericRepository<Artist> repository)
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
        public IHttpActionResult Post([FromBody]ArtistRequestModel artist)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var newArtist = new Artist
            {
                Name = artist.Name,
                Country = artist.Country,
                DateOfBirth = artist.DateOfBirth,
                Albums = artist.Albums
            };

            this.repository.Insert(newArtist);
            this.repository.SaveChanges();

            return this.Ok(newArtist);
        }

        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]ArtistRequestModel artist)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var currentArtist = this.repository.SelectById(id);

            if (currentArtist == null)
            {
                return this.BadRequest("Invalid Id");
            }

            currentArtist.Name = string.IsNullOrEmpty(artist.Name) ? currentArtist.Name : artist.Name;
            currentArtist.DateOfBirth = artist.DateOfBirth;
            currentArtist.Country = string.IsNullOrEmpty(artist.Country) ? currentArtist.Country : artist.Country;

            this.repository.Update(currentArtist);
            this.repository.SaveChanges();
            return this.Ok(currentArtist);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var artist = this.repository.SelectById(id);

            if (artist == null)
            {
                return this.BadRequest("Invalid id");
            }

            this.repository.Delete(id);
            this.repository.SaveChanges();
            return this.Ok();
        }
    }
}