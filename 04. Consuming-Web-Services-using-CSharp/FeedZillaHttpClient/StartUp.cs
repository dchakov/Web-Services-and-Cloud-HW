namespace FeedZillaHttpClient
{
    using FeedZilla;
    using Newtonsoft.Json;
    using System;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;

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

            string uriFaroo = "http://www.faroo.com/api?q=" + qureyString + "&start=1&length=" + count + "&l=en&src=news&f=json";
            string uriFeedzilla = "http://api.feedzilla.com/v1/categories/26/articles/search.json?q=" + qureyString;

            SearchForArticlesHttpClient(qureyString, count, uriFaroo);
            SearchForArticlesHttpWebRequest(qureyString, count, uriFaroo);
        }

        private static void SearchForArticlesHttpClient(string qureyString, int count, string uri)
        {
            Console.WriteLine("Articles:");
            using (var client = new HttpClient())
            {
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

        private static void SearchForArticlesHttpWebRequest(string qureyString, int count, string uri)
        {
            Console.WriteLine("Articles:");


            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                using (var responseStream = request.GetResponse().GetResponseStream())
                {
                    using (var reader = new StreamReader(responseStream))
                    {
                        var fzResult = JsonConvert.DeserializeObject<FZResult>(reader.ReadToEnd());

                        fzResult.Articles
                            .Take(count)
                            .ToList()
                            .ForEach(a => Console.WriteLine("{0} {1}", a.Title, a.Url));
                    }
                }
            }
            catch (WebException ex)
            {
                var response = ex.Response as HttpWebResponse;
                Console.WriteLine(response.StatusCode);
            }
        }
    }
}
