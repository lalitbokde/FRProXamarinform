using System;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;

using Xamarin.Forms;
using Plugin.Share;
using Acr.UserDialogs;
using FR.Resx;

namespace FR
{
    public partial class ModalChatMessageActionPage : PopupPage
    {
        public string mMessage = "";
        public string mId = "";

        public ModalChatMessageActionPage(string message, string messageId)
        {
            InitializeComponent();
            this.mMessage = message;
            this.mId = messageId;
        }

        private void OnClose(object sender, EventArgs e)
        {
            PopupNavigation.PopAsync();
        }

        void CopyOnTapped(object sender, EventArgs args)
        {
            CrossShare.Current.SetClipboardText(mMessage);

            ToastConfig tc = new ToastConfig(AppResources.MessageAlreadyCopy)
                .SetDuration(TimeSpan.FromSeconds(3));

            UserDialogs.Instance.Toast(tc);

            PopupNavigation.PopAsync();
        }

        void ShareOnTapped(object sender, EventArgs args)
        {
            CrossShare.Current.Share(new Plugin.Share.Abstractions.ShareMessage
            {
                Text = mMessage,
                Title = AppResources.Share
            });
            PopupNavigation.PopAsync();
        }

        async void FavouriteOnTapped(object sender, EventArgs args)
        {
            Favourite newFavourite = await App.FavouriteManager.Create(App.mUser.ID, Convert.ToInt32(mId));
            var newfavouriteID = newFavourite.ID;
            ToastConfig tc = new ToastConfig(AppResources.MessageFavAdded)
              .SetDuration(TimeSpan.FromSeconds(3));

            UserDialogs.Instance.Toast(tc);

            await PopupNavigation.PopAsync();
        }
    }
}
