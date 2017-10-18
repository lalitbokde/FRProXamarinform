using FR.Resx;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace FR
{
    public partial class DrawerPage : ContentPage
    {
        public Action<ContentPage> OnMenuSelect{get;set;}


        public DrawerPage()
        {
            InitializeComponent();

            App.mDrawerPage = this;

            var categories = new List<Category>()
            {
                new Category(AppResources.ChattingRoom, () => new ChatRoomListPage(),"chat_room.png"),
                new Category(AppResources.FRChat, () => new FRMemberChatListPage(),"fr_chat.png"),
                new Category(AppResources.MyFriends, () => new MyFriends(),"friends.png"),
                new Category(AppResources.MyProfile, () => new MyProfilePage(),"user_profile.png"),
                new Category(AppResources.MenuFavourite, () => new FavouriteListPage(),"fav.png"),
            };

            if (App.mIsUserAllowToEnroll)
            {
                categories.Add(new Category(AppResources.Enroll, () => new EnrollListPage(1), "enroll.png"));
            }

            categories.Add(new Category(AppResources.Logout, () => new Login(), "logout.png"));

            listed.BackgroundColor = Color.White;

            SetProfileImage(Constants.Image);

            myName.Text = AppResources.Welcome + Constants.Username;

            listed.ItemsSource = categories;


            listed.ItemSelected += (object sender, SelectedItemChangedEventArgs e) =>
            {
                if (OnMenuSelect != null)
                {
                    var category = (Category)e.SelectedItem;
                    if (category.Name == "Logout")
                    {
                        App.Current.MainPage = new NavigationPage(new Login())
                        {
                            BarBackgroundColor = Color.FromHex("#ffffff"),
                            BarTextColor = Color.Black
                        };
                    }
                    var categoryPage = category.PageFn();
                    OnMenuSelect(categoryPage);
                }
            };
        }

        public void SetProfileImage(string imgPath) {
            if (App.mUser.Avatar == null || App.mUser.Avatar == "")
            {
                imgPath = "https://lh3.googleusercontent.com/-0Olet6FXcxA/AAAAAAAAAAI/AAAAAAAAAAA/3_ZjPngHGYQ/s128-c-k/photo.jpg";
            }
            else
            {
                imgPath = Constants.host + "/" + App.mUser.Avatar;
            }

            imgSideMenuProfile.Source = new UriImageSource
            {
                Uri = new Uri(imgPath),
                CachingEnabled = true,
                CacheValidity = new TimeSpan(1, 0, 0, 0) //Caching image for a day
            };
        }
    }
}
