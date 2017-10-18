using System;
using Newtonsoft.Json;

namespace FR
{
    public class Favourite
    {
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }

        [JsonProperty(PropertyName = "user_id")]
        public int User_ID { get; set; }

        [JsonProperty(PropertyName = "chat_message_id")]
        public int Chat_Message_ID { get; set; }

        [JsonProperty(PropertyName = "message")]
        public String Message { get; set; }

        [JsonProperty(PropertyName = "file")]
        public String File { get; set; }
    }
}
