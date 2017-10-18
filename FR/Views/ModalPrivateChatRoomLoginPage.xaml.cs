using Acr.UserDialogs;
using FR.Resx;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms;

namespace FR
{
    public partial class ModalPrivateChatRoomLoginPage : PopupPage
    {
        public ChatRoom mChatRoom { get; set;}
        private INavigation mNavigation;

        public ModalPrivateChatRoomLoginPage(ChatRoom chatRoom, INavigation navigation)
        {
            InitializeComponent();
            mChatRoom = chatRoom;
            mNavigation = navigation;
        }

        private void OnClickedConfirm(object sender, EventArgs e)
        {
            if (etyPassword.Text == mChatRoom.Password)
            {
                var chatRoomItemPage = new ChatRoomItemPage(mChatRoom);
                PopupNavigation.PopAsync();
                mNavigation.PushAsync(chatRoomItemPage);
            }
            else
            {
                ToastConfig tc = new ToastConfig(AppResources.PasswordIncorrect)
                .SetDuration(TimeSpan.FromSeconds(3));

                UserDialogs.Instance.Toast(tc);
            }
        }
    }
}
