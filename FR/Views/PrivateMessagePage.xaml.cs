using System;
using Plugin.Media;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using FR.Resx;
using FR.ViewModels;

namespace FR
{
    public partial class PrivateMessagePage : ContentPage
    {
        public ChatRoom chatRoom { get; private set; }
        public ChatMessageListResponseResult chatMessagesResponseResult { get; private set; }
        public ObservableCollection<ChatMessage> chatMessages { get; private set; }
        public bool isRecord { get; private set; }
        public Friend friend { get; private set; }

        public PrivateMessagePage(Friend friend)
        {
            InitializeComponent();
            this.Title = String.Format("{0} {1}", AppResources.SendPMTo, friend.FriendName);
            //this.chatRoom = chatRoom;
            this.friend = friend;
            isRecord = false;

            imgSend.Source = "send.png";
            imgVoice.Source = "sound.png";
            imgPicture.Source = "image.png";
            imgCamera.Source = "camera.png";
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            IsBusy = true;
            chatMessagesResponseResult = await App.ChatMessageManager.GetPrivateMessagesResultPage(friend.RelationshipID, 1, 15, 0);
            this.chatMessages = chatMessagesResponseResult.ChatMessages;
            BindingContext = new ChatMessagePMViewModel(this, chatMessages, friend.RelationshipID);
            if (chatMessages.Count > 0)
            {
                lvChatMessages.ItemsSource = chatMessages;

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
            ObservableCollection<ChatMessage> newChatMessages = await App.ChatMessageManager.GetNewPrivateMessages(friend.RelationshipID, App.mUser.ID);
            if (newChatMessages.Count > 0)
            {
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
        }

        async void OnTappedImgSend(object sender, EventArgs args)
        {
            var message = etyMessage.Text;

            ChatMessage chatmessage = new ChatMessage();
            chatmessage.Sender_User_ID = App.mUser.ID;
            chatmessage.Receiver_User_ID = friend.FriendID;
            chatmessage.Message = message;

            IsBusy = true;
            ChatMessage chatMessage = await App.ChatMessageManager.CreatePrivateMessage(chatmessage);
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
            ChatMessage chatMessage = await App.ChatMessageManager.UploadPMImage(file, App.mUser.ID, friend.FriendID);
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
            ChatMessage chatMessage = await App.ChatMessageManager.UploadPMImage(file, App.mUser.ID, friend.FriendID);
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
                ChatMessage chatMessage = await App.ChatMessageManager.UploadPMVoice(audioFile, App.mUser.ID, friend.FriendID);
                chatMessages.Add(chatMessage);
                IsBusy = false;

                var v = lvChatMessages.ItemsSource.Cast<object>().LastOrDefault();
                lvChatMessages.ScrollTo(v, ScrollToPosition.End, true);

                isRecord = false;
            }

        }

    }
}
