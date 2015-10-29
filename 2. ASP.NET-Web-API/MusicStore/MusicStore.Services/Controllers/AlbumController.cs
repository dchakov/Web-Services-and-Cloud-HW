namespace MusicStore.Services.Controllers
{
    using Data.Repositories;
    using Models;
    using MusicStore.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    public class AlbumController : ApiController
    {
        private IGenericRepository<Album> repository = null;

        public AlbumController()
        {
            this.repository = new GenericRepository<Album>();
        }

        public AlbumController(IGenericRepository<Album> repository)
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
        public IHttpActionResult Post([FromBody]AlbumRequestModel album)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var newAlbum = new Album
            {
                Title = album.Title,
                Year = album.Year,
                Producer = album.Producer,
                Artists = album.Artists,
                Songs = album.Songs
            };

            this.repository.Insert(newAlbum);
            this.repository.SaveChanges();

            return this.Ok(newAlbum);
        }

        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]AlbumRequestModel album)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var currentAlbum = this.repository.SelectById(id);

            if (currentAlbum == null)
            {
                return this.BadRequest("Invalid Id");
            }

            currentAlbum.Title = string.IsNullOrEmpty(album.Title) ? currentAlbum.Title : album.Title;
            currentAlbum.Year = album.Year;
            currentAlbum.Producer = string.IsNullOrEmpty(album.Producer) ? currentAlbum.Producer : album.Producer;

            this.repository.Update(currentAlbum);
            this.repository.SaveChanges();
            return this.Ok(currentAlbum);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var album = this.repository.SelectById(id);

            if (album == null)
            {
                return this.BadRequest("Invalid id");
            }

            this.repository.Delete(id);
            this.repository.SaveChanges();
            return this.Ok();
        }
    }
}
