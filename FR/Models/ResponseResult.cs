using Newtonsoft.Json;

namespace FR
{
    public class ResponseResult
    {
        [JsonProperty(PropertyName = "success")]
        public bool Success { get; set; }

        [JsonProperty(PropertyName = "error")]
        public string Error { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
    }
}
