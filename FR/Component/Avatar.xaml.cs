using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace FR.Component
{
    public partial class Avatar : StackLayout
    {
        #region Text

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create<Avatar, string>(
            p => p.lblName.Text,
            "",
            BindingMode.OneWay,
            null,
            new BindableProperty.BindingPropertyChangedDelegate<string>(HandleTextChanged));

        static void HandleTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue == null)
            {
                return;
            }

            ((Avatar)bindable).lblName.Text = newValue.ToString();
        }

        #endregion

        #region ImageSource
        public string ImageSource { get; set; }

        public static readonly BindableProperty ImageSourceProperty = 
            BindableProperty.Create<Avatar, string>(
            p => p.ImageSource,
            "",
            BindingMode.OneWay,
            null,
            new BindableProperty.BindingPropertyChangedDelegate<string>(HandleImageSourceTextChanged));

        static void HandleImageSourceTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            string url = "";
            if (newValue == null || newValue.ToString() == "")
            {
                url = "https://lh3.googleusercontent.com/-0Olet6FXcxA/AAAAAAAAAAI/AAAAAAAAAAA/3_ZjPngHGYQ/s128-c-k/photo.jpg";
            }
            else
            {
                url = Constants.host + "/" + newValue;
            }
            ((Avatar)bindable).imgAvatar.Source = url;
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
                    ((Avatar)bindable).imgAvatar.IsVisible = false;
                    ((Avatar)bindable).lblName.IsVisible = false;
                }
                else
                {
                    ((Avatar)bindable).imgAvatar.IsVisible = true;
                    ((Avatar)bindable).lblName.IsVisible = true;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error Message: " + ex.Message);
            }
        }
        #endregion

        public Avatar()
        {
            InitializeComponent();
        }
    }
}
