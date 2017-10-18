using FR.Resx;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FR.ViewModels;
using Xamarin.Forms;
using Acr.UserDialogs;

namespace FR
{
    public partial class EnrollListPage : ContentPage
    {
        public int mCurrentPage { get; set; }

        public EnrollListPage(int currentPage)
        {
            InitializeComponent();
            this.Title = AppResources.Enroll;
            mCurrentPage = currentPage;

            btnAddEnroll.Clicked += (sender, args) =>
            {
                var addEnrollPage = new AddEnrollPage();
                Navigation.PushAsync(addEnrollPage);
            };

            App.mNavigation = Navigation;

            MessagingCenter.Subscribe<EnrollItemViewModel>(this, "Loading", (sender) =>
            {
                IsBusy = true;

            });

            MessagingCenter.Subscribe<EnrollItemViewModel>(this, "FinishLoading", (sender) =>
            {
                IsBusy = false;

            });

            MessagingCenter.Subscribe<EnrollItemViewModel, string>(this, "Info", (sender, arg) =>
           {
               if (arg == AppResources.PhotosNotSupported)
               {
                   DisplayAlert(AppResources.PhotosNotSupported, AppResources.PermissionNotGrantedToPhotos, AppResources.OK);
               }

           });

            MessagingCenter.Subscribe<EnrollItemViewModel, string>(this, "Update", (sender, arg) =>
            {
                ToastConfig tc = new ToastConfig(arg).SetDuration(TimeSpan.FromSeconds(3));
                UserDialogs.Instance.Toast(tc);

                listRefresh();
            });

            MessagingCenter.Subscribe<EnrollItemViewModel, string>(this, "Upload", (sender, arg) =>
            {
                ToastConfig tc = new ToastConfig(arg).SetDuration(TimeSpan.FromSeconds(3));
                UserDialogs.Instance.Toast(tc);

                listRefresh();
            });

            MessagingCenter.Subscribe<EnrollItemViewModel, string>(this, "Delete", (sender, arg) =>
            {
                ToastConfig tc = new ToastConfig(arg).SetDuration(TimeSpan.FromSeconds(3));
                UserDialogs.Instance.Toast(tc);

                listRefresh();
            });
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            IsBusy = true;

            if (mCurrentPage == 0)
            {
                mCurrentPage = 1;
            }

            EnrollListResponseResult enrollListResponseResult = await App.EnrollManager.GetAllByPage(mCurrentPage, 10);
            BindingContext = new EnrollListViewModel(enrollListResponseResult, mCurrentPage);

            IsBusy = false;
        }

        protected override void OnDisappearing()
        {
            MessagingCenter.Unsubscribe<EnrollItemViewModel, string>(this, "Error");
            MessagingCenter.Unsubscribe<EnrollItemViewModel, int>(this, "Update");
            MessagingCenter.Unsubscribe<EnrollItemViewModel, int>(this, "Upload");
            MessagingCenter.Unsubscribe<EnrollItemViewModel, int>(this, "Delete");
            MessagingCenter.Unsubscribe<EnrollItemViewModel>(this, "Loading");
            MessagingCenter.Unsubscribe<EnrollItemViewModel>(this, "FinishLoading");
        }

        public void listRefresh()
        {
            var mainPage = new MenuPage();
            var drawerPage = new DrawerPage();
            drawerPage.OnMenuSelect = (categoryPage) =>
            {
                mainPage.Detail = new NavigationPage(categoryPage)
                {
                    BarBackgroundColor = Color.FromHex("#ffffff"),
                    BarTextColor = Color.Black
                };
                mainPage.IsPresented = false;
            };
            mainPage.Master = drawerPage;

            mainPage.Detail = new NavigationPage(new EnrollListPage(mCurrentPage))
            {
                BarBackgroundColor = Color.FromHex("#ffffff"),//your color here
                BarTextColor = Color.Black
            };
            App.Current.MainPage = mainPage;
        }

    }
}
