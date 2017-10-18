using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace FR.ViewModels
{
    public class FriendRequestViewModel : BaseViewModel
    {
        public List<Relationship> relationships { get; private set; }

        private ObservableCollection<FriendRequestItemViewModel> myNewFriendRequests;
        public ObservableCollection<FriendRequestItemViewModel> MyNewFriendRequests
        {
            get { return myNewFriendRequests; }
            set { SetField(ref myNewFriendRequests, value); }
        }

        public FriendRequestViewModel(List<Relationship> relationships, INavigation navigation)
        {
            myNewFriendRequests = new ObservableCollection<FriendRequestItemViewModel>();

            foreach (Relationship relationship in relationships)
            {
                Friend friend = new Friend();
                if (relationship.User_One_ID == App.mUser.ID)
                {
                    friend.RelationshipID = relationship.ID;
                    friend.FriendID = relationship.User_Two_ID;
                    friend.FriendName = relationship.User_Two_Username;
                    friend.Status = relationship.Status;
                    friend.Avatar = relationship.User_Two_Avatar;
                }
                else
                {
                    friend.RelationshipID = relationship.ID;
                    friend.FriendID = relationship.User_One_ID;
                    friend.FriendName = relationship.User_One_Username;
                    friend.Status = relationship.Status;
                    friend.Avatar = relationship.User_One_Avatar;

                }

                FriendRequestItemViewModel friendRequestItemViewModel = new FriendRequestItemViewModel(this, navigation);
                friendRequestItemViewModel.friend = friend;
                myNewFriendRequests.Add(friendRequestItemViewModel);
            }

        }

    }
}
