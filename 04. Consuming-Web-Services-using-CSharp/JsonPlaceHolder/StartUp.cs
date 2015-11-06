namespace JsonPlaceHolder
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;

    public class StartUp
    {
        public static void Main()
        {
            int count = 10;
            string qureyString = "quis";

            SearchForArticles(qureyString, count);
        }

        private static void SearchForArticles(string qureyString, int count)
        {
            Console.WriteLine("Albums....");
            using (var client = new HttpClient())
            {
                string uri = "http://jsonplaceholder.typicode.com/photos";

                client.BaseAddress = new Uri(uri);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("").Result;

                if (response.IsSuccessStatusCode)
                {
                    var articles = response.Content.ReadAsStringAsync().Result;
                    var articlesCollection = JsonConvert.DeserializeObject<List<Album>>(articles);
                    articlesCollection
                        .Where(a => a.Title.Contains(qureyString))
                        .Take(count)
                        .ToList()
                        .ForEach(a => Console.WriteLine("{0} {1}", a.Title, a.Url));
                }
                else
                {
                    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                }
            }
        }
    }
}
