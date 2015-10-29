namespace MusicStore.Client
{
    using Models;
    using MusicStore.Data;
    using MusicStore.Data.Migrations;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;

    public class Startup
    {
        public static void Main()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MusicStoreDbContext, Configuration>());

            //var db = new MusicStoreDbContext();
            //db.Albums.Count();

            var client = new HttpClient()
            {
                BaseAddress = new Uri("http://localhost:57271/")
            };

            GetAllAlbums(client);

        }

        private static void GetAllAlbums(HttpClient client)
        {
            using (client)
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response =  client.GetAsync("api/Album").Result;

                if (response.IsSuccessStatusCode)
                {
                    var albums = response.Content.ReadAsAsync<IEnumerable<Album>>().Result;
                    foreach (var album in albums)
                    {
                        Console.WriteLine("{0,4} {1,-20} {2}",
                            album.Id, album.Title, album.Producer);
                    }
                }
                else
                    Console.WriteLine("{0} ({1})",
                        (int)response.StatusCode, response.ReasonPhrase);

            }
        }
    }
}
