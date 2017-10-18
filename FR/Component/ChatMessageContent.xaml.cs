using System;
using System.Diagnostics;
using System.IO;
using Xamarin.Forms;
using Rg.Plugins.Popup.Extensions;
using System.Text.RegularExpressions;

namespace FR.Component
{
    public partial class ChatMessageContent : StackLayout
    {

        static string[] imageExtensions = {
            ".PNG", ".JPG", ".JPEG", ".BMP", ".GIF"
        };

        static string[] audioExtensions = {
            ".WAV", ".MID", ".MIDI", ".WMA", ".MP3", ".OGG", ".RMA", //etc
            ".AVI", ".MP4", ".DIVX", ".WMV", //etc
        };

        static bool IsImageFile(string path)
        {
            return -1 != Array.IndexOf(imageExtensions, Path.GetExtension(path).ToUpperInvariant());
        }

        static bool IsAudioFile(string path)
        {
            return -1 != Array.IndexOf(audioExtensions, Path.GetExtension(path).ToUpperInvariant());
        }


        #region Text
        public static readonly BindableProperty TextProperty =
           BindableProperty.Create<ChatMessageContent, string>(
           p => p.lblMessage.Text,
           "",
           BindingMode.OneWay,
           null,
           new BindableProperty.BindingPropertyChangedDelegate<string>(HandleTextChanged));

        static void HandleTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((ChatMessageContent)bindable).lblMessage.Text = newValue.ToString();
            ((ChatMessageContent)bindable).lblMessage.IsVisible = true;

            if (IsUrlValid(newValue.ToString()))
            {
                ((ChatMessageContent)bindable).lblMessage.TextColor= Color.Blue;
            }

        }
        #endregion

        #region MediaFileName
        public string MediaFileName { get; set; }

        public static readonly BindableProperty MediaFileNameProperty =
            BindableProperty.Create<ChatMessageContent, string>(
            p => p.MediaFileName,
            "",
            BindingMode.OneWay,
            null,
            new BindableProperty.BindingPropertyChangedDelegate<string>(HandleMediaFileNameTextChanged));

        static void HandleMediaFileNameTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            try
            {
                string filePath = "";
                if (newValue == null || newValue.ToString() == "")
                {
                    filePath = "";
                    return;
                }
                else
                {
                    filePath = Constants.host + "/" + newValue;
                }

                if (IsImageFile(filePath))
                {
                    ((ChatMessageContent)bindable).imgUploadPicture.Source = filePath;
                    ((ChatMessageContent)bindable).tgrImgUploadPicture.CommandParameter = filePath;
                    ((ChatMessageContent)bindable).imgUploadPicture.IsVisible = true;

                    ((ChatMessageContent)bindable).imgPlay.IsVisible = false;
                }
                else if (IsAudioFile(filePath))
                {
                    ((ChatMessageContent)bindable).imgUploadPicture.IsVisible = false;

                    ((ChatMessageContent)bindable).tgImgPlay.CommandParameter = filePath;
                    ((ChatMessageContent)bindable).imgPlay.IsVisible = true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error Message: " + ex.Message);
            }
        }
        #endregion

        #region SenderUserID
        public string SenderUserID { get; set; }

        public static readonly BindableProperty SenderUserIDProperty =
            BindableProperty.Create<ChatMessageContent, string>(
            p => p.SenderUserID,
            "",
            BindingMode.OneWay,
            null,
            new BindableProperty.BindingPropertyChangedDelegate<string>(HandleSenderUserIDTextChanged));

        static void HandleSenderUserIDTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            try
            {
                int senderUserID = 0;
                int.TryParse(newValue.ToString(), out senderUserID);
                if (senderUserID == App.mUser.ID)
                {
                    ((ChatMessageContent)bindable).lblMessage.TextColor = Color.FromHex("#d7e6fb");
                    ((ChatMessageContent)bindable).imgBubbleMessage.Source = Forms9Patch.ImageSource.FromMultiResource("FR.Resources.BubbleInternal.9.png");
                    ((ChatMessageContent)bindable).imgPlay.Source = "audio_play_2.png";
                }
                else
                {
                    ((ChatMessageContent)bindable).lblMessage.TextColor = Color.FromHex("#000000");
                    ((ChatMessageContent)bindable).imgBubbleMessage.Source = Forms9Patch.ImageSource.FromMultiResource("FR.Resources.BubbleExternal.9.png");
                    ((ChatMessageContent)bindable).imgPlay.Source = "audio_play_1.png";
                }

                //if (IsUrlValid(newValue.ToString()))
                //{
                //    ((ChatMessageContent)bindable).lblMessage.TextColor = Color.FromHex("#d7e6fb");
                //}

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error Message: " + ex.Message);
            }
        }
        #endregion

        #region MessageID
        public string MessageID { get; set; }

        public static readonly BindableProperty MessageIDProperty =
            BindableProperty.Create<ChatMessageContent, string>(
            p => p.lblMessage.FontFamily,
            "",
            BindingMode.OneWay,
            null,
            new BindableProperty.BindingPropertyChangedDelegate<string>(HandleMessageIDTextChanged));

        static void HandleMessageIDTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            try
            {
                int senderMessageID = 0;
                int.TryParse(newValue.ToString(), out senderMessageID);
                ((ChatMessageContent)bindable).lblMessage.FontFamily = newValue.ToString();

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error Message: " + ex.Message);
            }
        }
        #endregion


        public ChatMessageContent()
        {
            InitializeComponent();
        }

        void OnTappedImgPlay(object sender, EventArgs args)
        {
            TappedEventArgs tea = (TappedEventArgs)args;
            string voiceUrl = (string)tea.Parameter;
            DependencyService.Get<ISoundRecorder>().StartPlaybackByUrl(voiceUrl);
        }

        async void OnTappedImgUploadPicture(object sender, EventArgs args)
        {
            TappedEventArgs tea = (TappedEventArgs)args;
            string imageUrl = (string)tea.Parameter;
            var page = new FullScreenImagePage(imageUrl);
            await App.mNavigation.PushAsync(page);
        }

        async void LongPressing(object sender, MR.Gestures.LongPressEventArgs e)
        {
            Label lblMessage = (Label)sender;
            var page = new ModalChatMessageActionPage(lblMessage.Text.Trim(),lblMessage.FontFamily);
            await Navigation.PushPopupAsync(page);
        }

        void TappedLabel(object sender, MR.Gestures.TapEventArgs e) {
            Label lblMessage = (Label)sender;
            bool isUri = IsUrlValid(lblMessage.Text.Trim());
            if (isUri)
            {
                Device.OpenUri(new Uri(lblMessage.Text.Trim()));
            }
        }

        static bool IsUrlValid(string url)
        {

            string pattern = @"^(http|https|ftp|)\://|[a-zA-Z0-9\-\.]+\.[a-zA-Z](:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*[^\.\,\)\(\s]$";
            Regex reg = new Regex(pattern, RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
            return reg.IsMatch(url);
        }
    }
}
