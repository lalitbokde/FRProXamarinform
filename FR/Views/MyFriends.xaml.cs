using FFImageLoading.Forms;
using FR.Resx;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using FR.ViewModels;

namespace FR
{
    public partial class MyFriends : ContentPage
    {
        public List<Relationship> relationships { get; private set; }

        public MyFriends()
        {
            InitializeComponent();
            bottomMenu.SetCurrentPage("MyFriends");
            this.Title = AppResources.MyFriends;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            IsBusy = true;

            relationships = await App.RelationshipManager.GetFriends(App.mUser.ID);
            BindingContext = new FriendViewModel(relationships, this.Navigation);

            IsBusy = false;
        }

        void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            String keywords = etySearchKeyword.Text.Trim();

            if (keywords == "")
                return;

            Navigation.PushAsync(new Views.SearchUserPage(keywords));

            etySearchKeyword.Text = "";
        }

        void OnTappedNewFriendRequest(object sender, EventArgs args)
        {
            Navigation.PushAsync(new NewFriendRequestListPage());
        }

    }
}
