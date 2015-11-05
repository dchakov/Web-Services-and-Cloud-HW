namespace FeedZillaHttpClient
{
    using FeedZilla;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;

    public class StartUp
    {
        public static void Main()
        {
            //Console.WriteLine("Please, enter query string for search");
            //string query = Console.ReadLine();
            //Console.WriteLine("Please, enter number of rezult for search");
            //int count = int.Parse(Console.ReadLine());

            int count = 10;
            string qureyString = "iphone";

            SearchForArticles(qureyString, count);
        }

        private static void SearchForArticles(string qureyString, int count)
        {
            Console.WriteLine("Articles:");
            using (var client = new HttpClient())
            {
                string uri = "http://www.faroo.com/api?q=" + qureyString + "&start=1&length=" + count + "&l=en&src=news&f=json";
                string uriFeedzilla = "http://api.feedzilla.com/v1/categories/26/articles/search.json?q=";
                client.BaseAddress = new Uri(uri);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("").Result;

                if (response.IsSuccessStatusCode)
                {
                    var articles = response.Content.ReadAsStringAsync().Result;
                    var articlesCollection = JsonConvert.DeserializeObject<FZResult>(articles);
                    articlesCollection.Articles
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
