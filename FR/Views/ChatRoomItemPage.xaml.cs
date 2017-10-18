using FR.Resx;
using Plugin.Media;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using FR.ViewModels;

using static FR.GenericUtilities.StringUtils;

namespace FR
{
    public partial class ChatRoomItemPage : ContentPage
    {
        public ChatRoom chatRoom { get; private set; }
        public ChatMessageListResponseResult chatMessagesResponseResult { get; private set; }
        public ObservableCollection<ChatMessage> chatMessages { get; private set; }
        public bool isRecord { get; private set; }

        public ChatRoomItemPage(ChatRoom chatRoom)
        {
            InitializeComponent();
            this.Title = "[" + chatRoom.ID.ToString() + "] " + CompactString(chatRoom.Name.ToString(), 25, CompactStringBehavior.removeEnd);
            this.chatRoom = chatRoom;
            isRecord = false;

            imgSend.Source = "send.png";
            imgVoice.Source = "sound.png";
            imgPicture.Source = "image.png";
            imgCamera.Source = "camera.png";
            imgFav.Source = "fav.png";

            App.mNavigation = Navigation;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            IsBusy = true;
            chatMessagesResponseResult = await App.ChatMessageManager.GetResultPageByChatRoomID(chatRoom.ID, 1, 15, 0);
            this.chatMessages = chatMessagesResponseResult.ChatMessages;
            BindingContext = new ChatMessageViewModel(this, chatMessages, chatRoom.ID);
            if (chatMessages.Count > 0)
            {
                var lastItem = lvChatMessages.ItemsSource.Cast<object>().LastOrDefault();
                lvChatMessages.ScrollTo(lastItem, ScrollToPosition.End, true);

            }
            IsBusy = false;

            //Timer trigger
            var intervalTime = TimeSpan.FromSeconds(30);
            Device.StartTimer(intervalTime, () =>
            {
                GetNewChatMessages();
                return true;
            }
            );

        }

        async void GetNewChatMessages()
        {
            ObservableCollection<ChatMessage> newChatMessages = await App.ChatMessageManager.GetNewChatMessage(chatRoom.ID, App.mUser.ID);
            foreach (ChatMessage newChatMessage in newChatMessages)
            {
                chatMessages.Add(newChatMessage);
            }

            lvChatMessages.ItemsSource = chatMessages;

            if (chatMessages.Count >= 5)
            {
                var v = lvChatMessages.ItemsSource.Cast<object>().LastOrDefault();
                lvChatMessages.ScrollTo(v, ScrollToPosition.End, true);
            }

        }

        async void OnTappedImgSend(object sender, EventArgs args)
        {
            var message = etyMessage.Text;

            ChatMessage chatmessage = new ChatMessage();
            chatmessage.Chat_Room_ID = chatRoom.ID;
            chatmessage.Message = message;
            chatmessage.Sender_User_ID = App.mUser.ID;
            chatmessage.Sender_Username = App.mUser.Username;
            chatmessage.Avatar = App.mUser.Avatar;

            IsBusy = true;
            ChatMessage chatMessage = await App.ChatMessageManager.Create(chatmessage);
            if (chatMessage != null)
            {
                chatMessages.Add(chatMessage);
                lvChatMessages.ItemsSource = chatMessages; //need to insert first message

                if (lvChatMessages.ItemsSource != null)
                {
                    var v = lvChatMessages.ItemsSource.Cast<object>().LastOrDefault();
                    lvChatMessages.ScrollTo(v, ScrollToPosition.End, true);
                }
            }

            IsBusy = false;

            etyMessage.Text = "";
        }

        async void OnTappedImgCamera(object sender, EventArgs args)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert(AppResources.NoCamera, AppResources.NoCameraAvailable, AppResources.OK);
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "FR",
                Name = "sample.jpg"
            });

            if (file == null)
                return;

            IsBusy = true;
            ChatMessage chatMessage = await App.ChatMessageManager.UploadImage(file, chatRoom.ID, App.mUser.ID);
            chatMessages.Add(chatMessage);
            IsBusy = false;

            var v = lvChatMessages.ItemsSource.Cast<object>().LastOrDefault();
            lvChatMessages.ScrollTo(v, ScrollToPosition.End, true);
        }

        async void OnTappedImgPicture(object sender, EventArgs args)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert(AppResources.PhotosNotSupported, AppResources.PermissionNotGrantedToPhotos, AppResources.OK);
                return;
            }
            var file = await CrossMedia.Current.PickPhotoAsync();


            if (file == null)
                return;

            IsBusy = true;
            ChatMessage chatMessage = await App.ChatMessageManager.UploadImage(file, chatRoom.ID, App.mUser.ID);
            chatMessages.Add(chatMessage);
            IsBusy = false;

            var v = lvChatMessages.ItemsSource.Cast<object>().LastOrDefault();
            lvChatMessages.ScrollTo(v, ScrollToPosition.End, true);
        }

        async void OnTappedImgVoice(object sender, EventArgs args)
        {
            if (isRecord == false)
            {
                imgVoice.Source = "mute.png";
                DependencyService.Get<ISoundRecorder>().StartRecording();
                isRecord = true;
            }
            else
            {
                imgVoice.Source = "sound.png";
                DependencyService.Get<ISoundRecorder>().EndRecordinging();
                byte[] audioFile = DependencyService.Get<ISoundRecorder>().GetAudio();

                IsBusy = true;
                ChatMessage chatMessage = await App.ChatMessageManager.UploadVoice(audioFile, chatRoom.ID, App.mUser.ID);
                chatMessages.Add(chatMessage);
                IsBusy = false;

                var v = lvChatMessages.ItemsSource.Cast<object>().LastOrDefault();
                lvChatMessages.ScrollTo(v, ScrollToPosition.End, true);

                isRecord = false;
            }

        }

        async void OnTappedImgFav(object sender, EventArgs args)
        {
            var page = new FavouriteListPage(chatRoom);
            await Navigation.PushAsync(page);
        }

        //async void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        //{
        //    var page = new ModalChatMessageActionPage();
        //    await Navigation.PushPopupAsync(page);
        //}
    }
}
