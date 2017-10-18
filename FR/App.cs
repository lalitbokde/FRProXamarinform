using System.Collections.Generic;
using Xamarin.Forms;

namespace FR
{
    public class App : Application
    {
        public static ChatRoomItemManager ChatRoomManager { get; private set; }
        public static UserManager UserManager { get; private set; }
        public static ChatMessageManager ChatMessageManager { get; private set; }
        public static RelationshipManager RelationshipManager { get; private set; }
        public static FavouriteManager FavouriteManager { get; private set; }
        public static EnrollManager EnrollManager { get; private set; }

        public static ITextToSpeech Speech { get; set; }

        public static bool IsUserLoggedIn { get; set; }
        public static User mUser { get; set;}
        public static List<RoleUser> mRoleUser { get; set; }
        public static bool mIsUserAllowToEnroll { get; set; }

        public static DrawerPage mDrawerPage { get; set; }

        public static INavigation mNavigation { get; set; }

        public App()
        {

            ChatRoomManager = new ChatRoomItemManager(new ChatRoomRestService());
            UserManager = new UserManager(new UserRestService());
            ChatMessageManager = new ChatMessageManager(new ChatMessageRestService());
            RelationshipManager = new RelationshipManager(new RelationshipRestService());
            FavouriteManager = new FavouriteManager(new FavouriteRestService());
            EnrollManager = new EnrollManager(new EnrollRestService());

            App.Current.MainPage = new NavigationPage(new Login())
            {
                BarBackgroundColor = Color.FromHex("#ffffff"),
                BarTextColor = Color.Black
            };
            mNavigation = App.Current.MainPage.Navigation;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

    }
}
