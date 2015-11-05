namespace FeedZilla
{
    using System;
    using System.IO;
    using System.Net;
    using Newtonsoft.Json;
    using System.Linq;

    public class StartUp
    {
        public static void Main()
        {
            int count = 10;
            string qureyString = "iphone";
            string uriFeedzilla = "http://api.feedzilla.com/v1/categories/26/articles/search.json?q=syria" + qureyString;
            string uri = "http://www.faroo.com/api?q=" + qureyString + "&start=1&length=" + count + "&l=en&src=news&f=json";

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
    }
}
