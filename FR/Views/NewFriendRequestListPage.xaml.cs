using FR.Resx;
using System.Collections.Generic;
using Xamarin.Forms;
using FR.ViewModels;

namespace FR
{
    public partial class NewFriendRequestListPage : ContentPage
    {
        public List<Relationship> newFriendsRequestRelationship { get; private set; }

        public NewFriendRequestListPage()
        {
            InitializeComponent();
            bottomMenu.SetCurrentPage("NewFriendRequestListPage");
            this.Title = AppResources.NewFriendRequestList;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            IsBusy = true;


            newFriendsRequestRelationship = await App.RelationshipManager.GetNewFriendsRequest(App.mUser.ID);
            BindingContext = new FriendRequestViewModel(newFriendsRequestRelationship, this.Navigation);
            IsBusy = false;
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            FriendRequestItemViewModel friendRequestItemViewModel = e.SelectedItem as FriendRequestItemViewModel;
            var friendAcceptOrRejectPage = new FriendAcceptOrRejectPage(friendRequestItemViewModel.friend);
            Navigation.PushAsync(friendAcceptOrRejectPage);
        }

    }
}
