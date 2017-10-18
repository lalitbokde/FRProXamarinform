using FR.Resx;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace FR
{
    public partial class FRMemberChatListPage : ContentPage
    {
        public FRMemberChatListPage()
        {
            InitializeComponent();
            bottomMenu.SetCurrentPage("ChatRoomListPage");
            this.Title = AppResources.FRChat;

            //if (App.mIsUserAllowToEnroll == false)
            //{
                lvChatRooms.Header = null;
            //}

            var tgr = new TapGestureRecognizer();
            tgr.Tapped += (s, e) => {
                var page = new EnrollListPage(1);
                Navigation.PushAsync(page);
            };
            gdEnroll.GestureRecognizers.Add(tgr);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            IsBusy = true;
            lvChatRooms.ItemsSource = await App.ChatRoomManager.GetTasksAsync((int)Enumerations.ChatRoomCategory.FRMember);
            IsBusy = false;
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ChatRoom chatRoom = e.SelectedItem as ChatRoom;
            var chatRoomItemPage = new ChatRoomItemPage(chatRoom);
            Navigation.PopAsync(true);
            Navigation.PushAsync(chatRoomItemPage);
         
        }

        protected override void OnDisappearing()
        {
            //base.OnDisappearing();
            //lvChatRooms.ItemsSource = null;
            //GC.Collect();
        }
    }
}
