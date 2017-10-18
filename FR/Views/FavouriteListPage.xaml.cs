using Acr.UserDialogs;
using FR.Resx;
using Plugin.Media;
using Plugin.Share;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

using static FR.GenericUtilities.StringUtils;

namespace FR
{
    public partial class FavouriteListPage : ContentPage
    {
        int chatroomid = 0;

        public FavouriteListPage()
        {
            InitializeComponent();
            this.Title = "My Favourites";
            App.mNavigation = Navigation;
        }

        public FavouriteListPage(ChatRoom chatRoom)
        {
            InitializeComponent();
            this.Title = "My Favourites";
            App.mNavigation = Navigation;
            chatroomid = chatRoom.ID;

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            IsBusy = true;
            GetFavList();
            IsBusy = false;
        }

        private async void GetFavList()
        {
            lvFav.ItemsSource = new ObservableCollection<Favourite>();
            int count = 0;
            ObservableCollection<Favourite> favouriteList = await App.FavouriteManager.GetByUser(App.mUser.ID);
            count = favouriteList.Count;
            this.Title = "My Favourites(" + count + ")";
            if (favouriteList.Count > 0)
            {
                lvFav.ItemsSource = favouriteList;
                var lastItem = lvFav.ItemsSource.Cast<object>().LastOrDefault();
                lvFav.ScrollTo(lastItem, ScrollToPosition.End, true);
            }
        }

        async void OnTappedlblFav(object sender, EventArgs args)
        {

            if (chatroomid != null || chatroomid != 0)
            {
                var mMessage = (Label)sender;
                var message = mMessage.Text;

                ChatMessage chatmessage = new ChatMessage();
                chatmessage.Chat_Room_ID = chatroomid;
                chatmessage.Message = message;
                chatmessage.Sender_User_ID = App.mUser.ID;
                chatmessage.Sender_Username = App.mUser.Username;
                chatmessage.Avatar = App.mUser.Avatar;

                ChatMessage chatMessage = await App.ChatMessageManager.Create(chatmessage);

                await this.Navigation.PopAsync();
            }
        }
        async void OnTappedDel(object sender, EventArgs e)
        {
            var ans = await DisplayAlert(AppResources.Question, AppResources.FavDeleteConfirmation, "Yes", "No");
            if (ans == true)
            {
                //Success condition
                var id = (TappedEventArgs)e;
                await App.FavouriteManager.Delete(Convert.ToInt32(id.Parameter));

                ToastConfig tc = new ToastConfig(AppResources.MessageFavDeleted)
                .SetDuration(TimeSpan.FromSeconds(3));
                UserDialogs.Instance.Toast(tc);

                GetFavList();
            }
            
        }
    }
}
