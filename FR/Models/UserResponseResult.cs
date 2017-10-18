using Newtonsoft.Json;
using System.Collections.Generic;

namespace FR
{
    public class UserResponseResult
    {
        [JsonProperty(PropertyName = "success")]
        public bool Success { get; set; }

        [JsonProperty(PropertyName = "error")]
        public string Error { get; set; }

        [JsonProperty(PropertyName = "result")]
        public User Result { get; set; }

        [JsonProperty(PropertyName = "roleUser")]
        public List<RoleUser> RoleUser { get; set; }
    }
}
