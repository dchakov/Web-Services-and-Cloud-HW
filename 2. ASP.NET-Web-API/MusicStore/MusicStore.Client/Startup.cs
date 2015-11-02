namespace MusicStore.Client
{
    using Models;
    using MusicStore.Data;
    using MusicStore.Data.Migrations;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Net.Http;
    using System.Net.Http.Headers;

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

            PostAlbum(client);

            UpdateAlbum(client);

            //DeleteAlbum(client);
        }

        private static async void UpdateAlbum(HttpClient client)
        {
            Console.WriteLine("Update album");
            using (client)
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var album = new Album()
                {
                    Title = "R",
                    Year = 2000
                };
                HttpResponseMessage response = await client.PostAsJsonAsync("api/album/2", album);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Post succsessful");
                }
                else
                {
                    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                }
            }
        }

        private static void PostAlbum(HttpClient client)
        {
            Console.WriteLine("Post album");
            using (client)
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var album = new Album()
                {
                    Title = "Rearviewmirror",
                    Year = 2009,
                    Producer = "Eddy"
                };
                HttpResponseMessage response = client.PostAsJsonAsync("api/album", album).Result;

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Post succsessful");
                }
                else
                {
                    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                }
            }
        }
        private static void GetAllAlbums(HttpClient client)
        {
            Console.WriteLine("All albums");
            using (client)
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("api/album").Result;

                if (response.IsSuccessStatusCode)
                {
                    var albums = response.Content.ReadAsAsync<IEnumerable<Album>>().Result;
                    foreach (var album in albums)
                    {
                        Console.WriteLine("{0,4} {1,-20} {2} {3}",
                            album.Id, album.Title, album.Producer, album.Year);
                    }
                }
                else
                {
                    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                }
            }
        }
    }
}
