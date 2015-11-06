namespace JsonPlaceHolder
{
    using Newtonsoft.Json;

    public class Album
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
