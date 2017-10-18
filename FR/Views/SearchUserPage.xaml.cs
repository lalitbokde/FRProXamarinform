using FR.Resx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace FR.Views
{
    public partial class SearchUserPage : ContentPage
    {
        public String keywords { get; private set; }
        public List<User> users { get; private set; }
        public List<User> displayUsers { get; private set; }

        public SearchUserPage(String keywords)
        {
            InitializeComponent();
            this.Title = AppResources.SearchUser;
            this.keywords = keywords;
            displayUsers = new List<User>();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            IsBusy = true;
            users = await App.UserManager.SearchUsers(keywords);
            foreach (User user in users)
            {
                user.Avatar = user.getAvatar();
                displayUsers.Add(user);
            }
            lvUsers.ItemsSource = displayUsers;
            IsBusy = false;
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var user = e.SelectedItem as User;
            var friendRequestPage = new FriendRequestPage(user);
            Navigation.PushAsync(friendRequestPage);
        }
    }
}
