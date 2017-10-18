using FR.Resx;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace FR
{
    public partial class ChatRoomCategoryListPage : ContentPage
    {
        public ChatRoomCategoryListPage()
        {
            InitializeComponent();
            bottomMenu.SetCurrentPage("ChatRoomListPage");
            this.Title = AppResources.ChattingRoom;

            if (App.mIsUserAllowToEnroll == false)
            {
                gdEnroll.IsVisible = false;
            }
            else
            {
                var tgrEnroll = new TapGestureRecognizer();
                tgrEnroll.Tapped += (s, e) =>
                {
                    var page = new EnrollListPage(1);
                    Navigation.PushAsync(page);
                };
                gdEnroll.GestureRecognizers.Add(tgrEnroll);
            }
            
            var tgrFRMemberArea = new TapGestureRecognizer();
            tgrFRMemberArea.Tapped += (s, e) => {
                var page = new FRMemberChatListPage();
                Navigation.PushAsync(page);
            };
            gdFRMemberArea.GestureRecognizers.Add(tgrFRMemberArea);

            var tgrCommonChatRoom = new TapGestureRecognizer();
            tgrCommonChatRoom.Tapped += (s, e) => {
                var page = new ChatRoomListPage();
                Navigation.PushAsync(page);
            };
            gdCommonChatRoom.GestureRecognizers.Add(tgrCommonChatRoom);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
        }

        //void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    ChatRoom chatRoom = e.SelectedItem as ChatRoom;
        //    if ((int)Enumerations.ChatRoomType.Public == chatRoom.Type)
        //    {
        //        var chatRoomItemPage = new ChatRoomItemPage(chatRoom);
        //        Navigation.PopAsync(true);
        //        Navigation.PushAsync(chatRoomItemPage);
        //    }
        //    else
        //    {
        //        var page = new ModalPrivateChatRoomLoginPage(chatRoom, Navigation);
        //        Navigation.PushPopupAsync(page);
        //    }
         
        //}

        protected override void OnDisappearing()
        {
        }
    }
}
