using System;

using Xamarin.Forms;

namespace FR.Component
{
    public partial class UserInfo : StackLayout
    {
        public string Username
        {
            get { return lblUsername.Text; }
            set { lblUsername.Text = value; }
        }

        public string Name
        {
            get { return lblName.Text; }
            set { lblName.Text = value; }
        }

        //public string TelNo
        //{
        //    get { return lblTelNo.Text; }
        //    set { lblTelNo.Text = value; }
        //}

        //public string IDCard
        //{
        //    get { return lblIDCard.Text; }
        //    set { lblIDCard.Text = value; }
        //}

        public string ImageProfile { get; set; }

        //public string BankName
        //{
        //    get { return lblBankName.Text; }
        //    set { lblBankName.Text = value; }
        //}

        //public string BankCardNumber
        //{
        //    get { return lblBankCardNumber.Text; }
        //    set { lblBankCardNumber.Text = value; }
        //}

        public UserInfo()
        {
            InitializeComponent();
        }

        public void ImageCache()
        {
            imgProfile.Source = new UriImageSource
            {
                Uri = new Uri(ImageProfile),
                CachingEnabled = true,
                CacheValidity = new TimeSpan(1, 0, 0, 0) //Caching image for a day
            };
        }
    }
}
