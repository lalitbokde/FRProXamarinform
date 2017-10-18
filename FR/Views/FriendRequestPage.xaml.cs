using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace FR.Views
{
    public partial class FriendRequestPage : ContentPage
    {
        public User searchUser { get; set; }

        public FriendRequestPage(User user)
        {
            InitializeComponent();
            this.searchUser = user;

            userInfo.Username = user.Username;
            userInfo.Name = user.Name;
            //userInfo.TelNo = user.Phone_Number;
            //userInfo.IDCard = user.Country_ID_Card;
            userInfo.ImageProfile = "https://lh3.googleusercontent.com/-0Olet6FXcxA/AAAAAAAAAAI/AAAAAAAAAAA/3_ZjPngHGYQ/s128-c-k/photo.jpg";
            userInfo.ImageCache();

            btnSendFriendRequest.Clicked += BtnSendFriendRequest_Clicked;
        }

        private async void BtnSendFriendRequest_Clicked(object sender, EventArgs e)
        {
            IsBusy = true;
            Relationship relationship = new Relationship();
            relationship.User_One_ID = App.mUser.ID;
            relationship.User_Two_ID = this.searchUser.ID;
            relationship.Status = 0;
            relationship.Action_User_ID = App.mUser.ID;

            await App.RelationshipManager.FriendRequest(relationship);
            IsBusy = false;
        }
    }
}
