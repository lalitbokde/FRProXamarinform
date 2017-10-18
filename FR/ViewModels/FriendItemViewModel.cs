using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace FR.ViewModels
{
    public class FriendItemViewModel : BaseViewModel
    {
        private readonly FriendViewModel friendViewModel;
        private INavigation _navigation;

        public Friend friend { get; set; }

        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set { SetField(ref isSelected, value); }
        }

        ICommand tapCommand;
        public ICommand TapCommand
        {
            get { return tapCommand; }
        }

        ICommand tapSendPrivateMessageCommand;
        public ICommand TapSendPrivateMessageCommand
        {
            get { return tapSendPrivateMessageCommand; }
        }

        public FriendItemViewModel(FriendViewModel friendViewModel, INavigation navigation)
        {
            // configure the TapCommand with a method
            tapCommand = new Command(OnTapped);
            tapSendPrivateMessageCommand = new Command(OnTappedSendPrivateMessage);

            _navigation = navigation;

            if (friendViewModel == null)
            {
                throw new ArgumentNullException("listViewModel");
            }
            this.friendViewModel = friendViewModel;
        }
        
        async void OnTapped(object parameter)
        {
            Friend friend = (Friend)parameter;
            friend.ActionUserId = App.mUser.ID;
            await App.RelationshipManager.FriendBlock(friend);
            friendViewModel.MyFriends.Remove(this);
        }

        void OnTappedSendPrivateMessage(object parameter)
        {
            Friend friend = (Friend)parameter;
            var privateMessagePage = new PrivateMessagePage(friend);

            _navigation.PushAsync(privateMessagePage);
        }
    }
}
