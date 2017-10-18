using Xamarin.Forms;
using Plugin.Media;
using FR.Resx;

namespace FR
{
    public partial class MyProfilePage : ContentPage
    {
        public MyProfilePage()
        {
            InitializeComponent();
            bottomMenu.SetCurrentPage("MyProfilePage");
            this.Title = AppResources.MyProfile;

            userInfo.Username = App.mUser.Username;
            userInfo.Name = App.mUser.Name;

            //userInfo.TelNo = App.mUser.Phone_Number;
            //userInfo.IDCard = App.mUser.Country_ID_Card;

            bankInfo.BankName = App.mUser.Bank_Name;
            bankInfo.BankCardNumber = App.mUser.Bank_Card_Number;

            if (App.mUser.Avatar == null || App.mUser.Avatar == "")
            {
                userInfo.ImageProfile = "https://lh3.googleusercontent.com/-0Olet6FXcxA/AAAAAAAAAAI/AAAAAAAAAAA/3_ZjPngHGYQ/s128-c-k/photo.jpg";
            }
            else
            {
                userInfo.ImageProfile = Constants.host + "/" + App.mUser.Avatar;
            }
            userInfo.ImageCache();



            btnChangeProfileImage.Clicked += async (sender, args) =>
            {
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await DisplayAlert(AppResources.PhotosNotSupported, AppResources.PermissionNotGrantedToPhotos, AppResources.OK);
                    return;
                }
                var file = await CrossMedia.Current.PickPhotoAsync();


                if (file == null)
                    return;

                IsBusy = true;
                User user = await App.UserManager.UploadProfileImage(file, App.mUser.ID);
                App.mUser = user;

                //reset user profile picture 
                userInfo.ImageProfile = Constants.host + "/" + App.mUser.Avatar;
                userInfo.ImageCache();

                //reset sidemenu profile picture
                DrawerPage dwpage = App.mDrawerPage;
                dwpage.SetProfileImage(App.mUser.Avatar);

                IsBusy = false;
            };

        }

    }
}
