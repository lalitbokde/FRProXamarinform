using System;
using Newtonsoft.Json;

namespace FR
{
    public class Enroll
    {
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }

        [JsonProperty(PropertyName = "sender_user_id")]
        public int Sender_User_ID { get; set; }

        [JsonProperty(PropertyName = "sender_username")]
        public string Sender_Username { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "telephone")]
        public string Telephone { get; set; }

        [JsonProperty(PropertyName = "remark")]
        public string Remark { get; set; }

        [JsonProperty(PropertyName = "file")]
        public string File { get; set; }

        [JsonProperty(PropertyName = "created_at")]
        public DateTime Created_at { get; set; }

        [JsonProperty(PropertyName = "updated_at")]
        public DateTime Updated_at { get; set; }

    }
}
