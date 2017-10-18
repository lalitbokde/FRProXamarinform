using Newtonsoft.Json;

namespace FR
{
    public class EnrollResponseResult
    {
        [JsonProperty(PropertyName = "success")]
        public bool Success { get; set; }

        [JsonProperty(PropertyName = "error")]
        public string Error { get; set; }

        [JsonProperty(PropertyName = "result")]
        public Enroll Result { get; set; }
    }
}
