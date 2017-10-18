using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace FR.ViewModels
{
    public class FriendViewModel: BaseViewModel
    {
        public List<Relationship> relationships { get; private set; }

        private ObservableCollection<FriendItemViewModel> myFriends;
        public ObservableCollection<FriendItemViewModel> MyFriends {
            get { return myFriends; }
            set { SetField(ref myFriends, value); }
        }

        public FriendViewModel(List<Relationship> relationships, INavigation navigation) {
            myFriends = new ObservableCollection<FriendItemViewModel>();
            
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

                FriendItemViewModel friendItemViewModel = new FriendItemViewModel(this, navigation);
                friendItemViewModel.friend = friend;
                myFriends.Add(friendItemViewModel);
            }

        }

        private FriendItemViewModel selectedItem;
        public FriendItemViewModel SelectedItem
        {
            get { return selectedItem; }

            set
            {
                if (selectedItem != value)
                {
                    //update previously selected item, if any :
                    if (selectedItem != null)
                        selectedItem.IsSelected = false;

                    //update currently selected item :
                    value.IsSelected = true;

                    selectedItem = value;
                    OnPropertyChanged("SelectedItem");
                }
            }

            
        }


    }
}
