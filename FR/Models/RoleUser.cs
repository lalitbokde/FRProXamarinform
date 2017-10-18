using System;
using Newtonsoft.Json;

namespace FR
{
    public class RoleUser
    {
        [JsonProperty(PropertyName = "user_id")]
        public int UserID { get; set; }

        [JsonProperty(PropertyName = "role_id")]
        public int RoleID { get; set; }
    }
}
