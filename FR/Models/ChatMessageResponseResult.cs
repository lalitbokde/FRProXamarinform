using Newtonsoft.Json;

namespace FR
{
    public class ChatMessageResponseResult
    {
        [JsonProperty(PropertyName = "success")]
        public bool Success { get; set; }

        [JsonProperty(PropertyName = "result")]
        public ChatMessage Result { get; set; }

    }
}
