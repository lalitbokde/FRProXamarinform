using FR.Resx;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace FR
{
    public partial class ChatRoomListPage : ContentPage
    {
        public ChatRoomListPage()
        {
            InitializeComponent();
            bottomMenu.SetCurrentPage("ChatRoomListPage");
            this.Title = AppResources.ChattingRoom;

            if (App.mIsUserAllowToEnroll == false)
            {
                lvChatRooms.Header = null;
            }

            //var tgr = new TapGestureRecognizer();
            //tgr.Tapped += (s, e) => {
            //    var page = new EnrollListPage(1);
            //    Navigation.PushAsync(page);
            //};
            //gdEnroll.GestureRecognizers.Add(tgr);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            IsBusy = true;
            lvChatRooms.ItemsSource = await App.ChatRoomManager.GetTasksAsync((int)Enumerations.ChatRoomCategory.Common);
            IsBusy = false;
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ChatRoom chatRoom = e.SelectedItem as ChatRoom;
            if ((int)Enumerations.ChatRoomType.Public == chatRoom.Type)
            {
                var chatRoomItemPage = new ChatRoomItemPage(chatRoom);
                Navigation.PopAsync(true);
                Navigation.PushAsync(chatRoomItemPage);
            }
            else
            {
                var page = new ModalPrivateChatRoomLoginPage(chatRoom, Navigation);
                Navigation.PushPopupAsync(page);
            }
         
        }

        protected override void OnDisappearing()
        {
            //base.OnDisappearing();
            //lvChatRooms.ItemsSource = null;
            //GC.Collect();
        }
    }
}
