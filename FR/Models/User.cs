namespace FR
{
    public class User
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Phone_Number { get; set; }
        public string Country_ID_Card { get; set; }
        public string Bank_Name { get; set; }
        public string Bank_Card_Number { get; set; }
        public string Avatar { get; set; }

        public string getAvatar()
        {
            string imgAvatarPath = "";

            if (this.Avatar == null || this.Avatar == "")
            {
                imgAvatarPath = "https://lh3.googleusercontent.com/-0Olet6FXcxA/AAAAAAAAAAI/AAAAAAAAAAA/3_ZjPngHGYQ/s128-c-k/photo.jpg";
            }
            else
            {
                imgAvatarPath = Constants.host + "/" + this.Avatar;
            }

            return imgAvatarPath;
        }
    }
}
