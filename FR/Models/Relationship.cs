using System;
using Newtonsoft.Json;

namespace FR
{
    public class Relationship
    {
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }

        [JsonProperty(PropertyName = "user_one_id")]
        public int User_One_ID { get; set; }

        [JsonProperty(PropertyName = "user_two_id")]
        public int User_Two_ID { get; set; }

        [JsonProperty(PropertyName = "status")]
        public int Status { get; set; }

        [JsonProperty(PropertyName = "action_user_id")]
        public int Action_User_ID { get; set; }

        [JsonProperty(PropertyName = "created_at")]
        public DateTime Created_at { get; set; }

        [JsonProperty(PropertyName = "updated_at")]
        public DateTime Updated_at { get; set; }

        [JsonProperty(PropertyName = "user_one_username")]
        public String User_One_Username { get; set; }

        [JsonProperty(PropertyName = "user_two_username")]
        public String User_Two_Username { get; set; }

        [JsonProperty(PropertyName = "action_user_username")]
        public String Action_User_Username { get; set; }

        [JsonProperty(PropertyName = "user_one_avatar")]
        public String User_One_Avatar { get; set; }

        [JsonProperty(PropertyName = "user_two_avatar")]
        public String User_Two_Avatar { get; set; }

        public Relationship() {
        }

        public Relationship(int id, int user_one_id, int user_two_id, int status, int action_user_id,
          DateTime created_at, DateTime updated_at, string user_one_username, string user_two_username, 
          string action_user_username, string user_one_avatar, string user_two_avatar)
        {
            ID = id;
            User_One_ID = user_one_id;
            User_Two_ID = user_two_id;
            Status = status;
            Action_User_ID = action_user_id;
            Created_at = created_at;
            Updated_at = updated_at;
            User_One_Username = user_one_username;
            User_Two_Username = user_two_username;
            Action_User_Username = action_user_username;
            User_One_Avatar = user_one_avatar;
            User_Two_Avatar = user_two_avatar;
        }
    }
}
