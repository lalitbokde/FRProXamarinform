using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace FR
{
    public partial class FriendAcceptOrRejectPage : ContentPage
    {
        public User targetUser { get; set; }
        public int targetFriendUserID { get; set; }
        public Friend friend { get; set; }

        public FriendAcceptOrRejectPage(Friend friend)
        {
            InitializeComponent();
            this.targetFriendUserID = friend.FriendID;
            this.friend = friend;
            btnRejectRequest.Clicked += BtnRejectRequest_Clicked;
            btnAcceptRequest.Clicked += BtnAcceptRequest_Clicked;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            IsBusy = true;
            var user = await App.UserManager.GetUser(this.targetFriendUserID);
            targetUser = user;

            userInfo.Username = user.Username;
            userInfo.Name = user.Name;
            //userInfo.TelNo = user.Phone_Number;
            //userInfo.IDCard = user.Country_ID_Card;
            userInfo.ImageProfile = "https://lh3.googleusercontent.com/-0Olet6FXcxA/AAAAAAAAAAI/AAAAAAAAAAA/3_ZjPngHGYQ/s128-c-k/photo.jpg";
            userInfo.ImageCache();

            IsBusy = false;
        }

        private async void BtnRejectRequest_Clicked(object sender, EventArgs e)
        {
            IsBusy = true;

            await App.RelationshipManager.RequestDecline(this.friend);
            IsBusy = false;

            await Navigation.PushAsync(new MyFriends());
        }

        private async void BtnAcceptRequest_Clicked(object sender, EventArgs e)
        {
            IsBusy = true;

            await App.RelationshipManager.RequestApprove(this.friend);
            IsBusy = false;

            await Navigation.PushAsync(new MyFriends());

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            GC.Collect();
        }
    }
}
