namespace MusicStore.Client
{
    using Models;
    using MusicStore.Data;
    using MusicStore.Data.Migrations;
    using Newtonsoft.Json;
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

            GetAllAlbums();

            PostAlbum();

            UpdateAlbum();

            DeleteAlbum(13);

            GetAllAlbumsXML();
        }

        private static void DeleteAlbum(int Id)
        {
            Console.WriteLine("Delete album");
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:57271/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.DeleteAsync("api/album/" + Id).Result;

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Delete succsessful");
                }
                else
                {
                    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                }
            }
        }

        private static void UpdateAlbum()
        {
            Console.WriteLine("Update album");
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:57271/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var album = new Album()
                {
                    Title = "New",
                    Year = 1997
                };
                HttpResponseMessage response = client.PutAsJsonAsync("api/album/2", album).Result;

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Update succsessful");
                }
                else
                {
                    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                }
            }
        }

        private static void PostAlbum()
        {
            Console.WriteLine("Post album");
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:57271/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var album = new Album()
                {
                    Title = "Rearviewmirror",
                    Year = 2009,
                    Producer = "Eddy"
                };

                var postContent = new StringContent(JsonConvert.SerializeObject(album));
                postContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = client.PostAsync("api/album", postContent).Result;

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

        private static void GetAllAlbums()
        {
            Console.WriteLine("All albums");
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:57271/");
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

        private static void GetAllAlbumsXML()
        {
            Console.WriteLine("All albums XML");
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:57271/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));

                HttpResponseMessage response = client.GetAsync("api/album").Result;

                if (response.IsSuccessStatusCode)
                {
                    var albums = response.Content.ReadAsStringAsync();
                    Console.WriteLine(albums.Result);
                   
                }
                else
                {
                    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                }
            }
        }
    }
}
