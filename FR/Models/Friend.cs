using Newtonsoft.Json;

namespace FR
{
    public class Friend
    {
        [JsonProperty(PropertyName = "id")]
        public int RelationshipID { get; set; }

        [JsonProperty(PropertyName = "friend_id")]
        public int FriendID { get; set; }

        [JsonProperty(PropertyName = "friend_name")]
        public string FriendName { get; set; }

        [JsonProperty(PropertyName = "status")]
        public int Status { get; set; }

        [JsonProperty(PropertyName = "action_user_id")]
        public int ActionUserId { get; set; }

        [JsonProperty(PropertyName = "avatar")]
        private string avatar;
        public string Avatar {
            get
            {
                return this.avatar;
            }
            set
            {
                if (value == null || value == "")
                {
                    this.avatar = "https://lh3.googleusercontent.com/-0Olet6FXcxA/AAAAAAAAAAI/AAAAAAAAAAA/3_ZjPngHGYQ/s128-c-k/photo.jpg";
                }
                else
                {
                    this.avatar = Constants.host + "/" + value;
                }
            }
        }
    }
}
