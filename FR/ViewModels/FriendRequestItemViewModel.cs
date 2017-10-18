using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace FR.ViewModels
{
    public class FriendRequestItemViewModel : BaseViewModel
    {
        private readonly FriendRequestViewModel friendRequestViewModel;
        private INavigation _navigation;

        public Friend friend { get; set; }

        ICommand approveFriendTapCommand;
        public ICommand ApproveFriendTapCommand
        {
            get { return approveFriendTapCommand; }
        }

        ICommand rejectFriendTapCommand;
        public ICommand RejectFriendTapCommand
        {
            get { return rejectFriendTapCommand; }
        }

        public FriendRequestItemViewModel(FriendRequestViewModel friendRequestViewModel, INavigation navigation)
        {
            // configure the TapCommand with a method
            approveFriendTapCommand = new Command(ApproveFriendOnTapped);
            rejectFriendTapCommand = new Command(RejectFriendOnTapped);

            _navigation = navigation;

            if (friendRequestViewModel == null)
            {
                throw new ArgumentNullException("FriendRequestItemViewModel");
            }
            this.friendRequestViewModel = friendRequestViewModel;
        }

        async void ApproveFriendOnTapped(object parameter)
        {
            Friend friend = (Friend)parameter;
            friend.ActionUserId = App.mUser.ID;
            await App.RelationshipManager.RequestApprove(friend);
            friendRequestViewModel.MyNewFriendRequests.Remove(this);
        }

        async void RejectFriendOnTapped(object parameter)
        {
            Friend friend = (Friend)parameter;
            friend.ActionUserId = App.mUser.ID;
            await App.RelationshipManager.RequestDecline(friend);
            friendRequestViewModel.MyNewFriendRequests.Remove(this);
        }
    }
}
