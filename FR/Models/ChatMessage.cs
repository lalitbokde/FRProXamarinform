using System;
using Newtonsoft.Json;
using System.ComponentModel;

namespace FR
{
    public class ChatMessage
    {
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }

        [JsonProperty(PropertyName = "chat_room_id")]
        public int? Chat_Room_ID { get; set; }

        [JsonProperty(PropertyName = "sender_user_id")]
        public int Sender_User_ID { get; set; }

        [JsonProperty(PropertyName = "sender_username")]
        public String Sender_Username { get; set; }

        [JsonProperty(PropertyName = "receiver_user_id")]
        public int? Receiver_User_ID { get; set; }

        [JsonProperty(PropertyName = "receiver_username")]
        public String Receiver_Username { get; set; }

        [JsonProperty(PropertyName = "message")]
        public String Message { get; set; }

        [JsonProperty(PropertyName = "file")]
        public String File { get; set; }

        [JsonProperty(PropertyName = "is_read")]
        public int Is_Read { get; set; }

        [JsonProperty(PropertyName = "created_at")]
        public DateTime Created_at { get; set; }

        [JsonProperty(PropertyName = "updated_at")]
        public DateTime Updated_at { get; set; }

        [JsonProperty(PropertyName = "avatar")]
        public string Avatar { get; set; }

        public ChatMessage() {
        }

        public ChatMessage(int id, int? chat_room_id, int sender_user_id, int? receiver_user_id, String message, 
            String file, int is_read, DateTime created_at, DateTime updated_at, String avatar, 
            String sender_username, String receiver_username)
        {
            ID = id;
            Chat_Room_ID = chat_room_id;
            Sender_User_ID = sender_user_id;
            Sender_Username = sender_username;
            Receiver_User_ID = receiver_user_id;
            Receiver_Username = receiver_username;
            Message = message;
            File = file;
            Is_Read = is_read;
            Created_at = created_at;
            Updated_at = updated_at;
            if (avatar == null || avatar == "")
            {
                Avatar = "https://lh3.googleusercontent.com/-0Olet6FXcxA/AAAAAAAAAAI/AAAAAAAAAAA/3_ZjPngHGYQ/s128-c-k/photo.jpg";
            }
            else
            {
                Avatar = Constants.host + "/" + avatar;
            }

        }
    }
}
