using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace FR.Component
{
    public partial class BottomMenu : StackLayout
    {
        public BottomMenu()
        {
            InitializeComponent();
        }

        public void SetCurrentPage(string pageName) {
            if (pageName == "MyProfilePage") {
                lblChattingRoom.TextColor = Color.FromHex("#c7cfda");
                lblMyFriends.TextColor = Color.FromHex("#c7cfda");
                lblMyProfile.TextColor = Color.FromHex("#1a75f9");

                imgChattingRoom.Source = "chat_room_unactive.png";
                imgMyFriends.Source = "my_friends_unactive.png";
                imgMyProfile.Source = "my_profile_active.png"; 
            } else if (pageName == "ChatRoomListPage") {
                lblChattingRoom.TextColor = Color.FromHex("#1a75f9");
                lblMyFriends.TextColor = Color.FromHex("#c7cfda");
                lblMyProfile.TextColor = Color.FromHex("#c7cfda");

                imgChattingRoom.Source = "chat_room_active.png";
                imgMyFriends.Source = "my_friends_unactive.png";
                imgMyProfile.Source = "my_profile_unactive.png";
            } else if (pageName == "MyFriends" || pageName == "NewFriendRequestListPage") {
                lblChattingRoom.TextColor = Color.FromHex("#c7cfda");
                lblMyFriends.TextColor = Color.FromHex("#1a75f9");
                lblMyProfile.TextColor = Color.FromHex("#c7cfda");

                imgChattingRoom.Source = "chat_room_unactive.png";
                imgMyFriends.Source = "my_friends_active.png";
                imgMyProfile.Source = "my_profile_unactive.png";
            }
        }

        void OnTappedImgChattingRoom(object sender, EventArgs args)
        {
            var chatRoomListPage = new ChatRoomListPage();
            Navigation.PopAsync(true);
            Navigation.PushAsync(chatRoomListPage);
        }

        void OnTappedImgMyFriends(object sender, EventArgs args)
        {
            var myFriendsPage = new MyFriends();
            Navigation.PopAsync(true);
            Navigation.PushAsync(myFriendsPage);
        }

        void OnTappedImgMyProfile(object sender, EventArgs args)
        {
            var myProfilePage = new MyProfilePage();
            Navigation.PopAsync(true);
            Navigation.PushAsync(myProfilePage);
        }
    }
}
