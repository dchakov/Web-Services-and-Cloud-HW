namespace FeedZilla
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class FZResult
    {
        [JsonProperty("articles")]
        public List<Article> Articles { get; set; }
    }
}
