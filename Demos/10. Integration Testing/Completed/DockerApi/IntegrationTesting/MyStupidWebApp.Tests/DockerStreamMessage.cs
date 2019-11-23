using Newtonsoft.Json;

namespace MyStupidWebApp.Tests
{
    public class DockerStreamMessage
    {
        [JsonProperty("stream")]
        public string Stream { get; set; }
    }
}