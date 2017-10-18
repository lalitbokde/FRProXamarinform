using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace FR
{
    public class ChatMessageListResponseResult
    {
        [JsonProperty(PropertyName = "success")]
        public bool Success { get; set; }

        [JsonProperty(PropertyName = "error")]
        public bool Error { get; set; }

        [JsonProperty(PropertyName = "total_page")]
        public int TotalPage { get; set; }

        [JsonProperty(PropertyName = "result")]
        public ObservableCollection<ChatMessage> ChatMessages { get; set; }
    }
}
