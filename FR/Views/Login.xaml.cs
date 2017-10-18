using FR.Resx;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace FR
{

    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);

			lblHost.Text = Constants.host.Replace("http://", "") + "( " + Constants.Version + " )";
			btnLogin.Clicked += async (s, e) =>
			{
                if (!string.IsNullOrEmpty(etyUsername.Text) && !string.IsNullOrEmpty(etyPassword.Text))
                {
                    Dictionary<string, string> credential = new Dictionary<string, string>();
                    credential.Add("Username", etyUsername.Text);
                    credential.Add("Password", etyPassword.Text);

                    IsBusy = true;
                    UserResponseResult userResponseResult = await App.UserManager.Authentication(credential);

                    if (userResponseResult.Success == false)
                    {
                        await DisplayAlert(AppResources.LoginFailed, userResponseResult.Error, AppResources.OK);
                        IsBusy = false;
                        return;
                    }

                    App.mUser = userResponseResult.Result;
                    App.mRoleUser = userResponseResult.RoleUser;

                    App.mIsUserAllowToEnroll = false;
                    foreach (RoleUser roleUser in App.mRoleUser)
                    {
                        if (roleUser.RoleID == (int)Enumerations.Role.VIPCPMUser || roleUser.RoleID == (int)Enumerations.Role.Guest)
                        {
                            App.mIsUserAllowToEnroll = true;
                        }
                    }

                    if (userResponseResult.Result != null && userResponseResult.Result.Name != null )
                    {
                        Constants.Username = etyUsername.Text;
                        App.Current.MainPage = new MenuPage();
                        await Navigation.PopAsync(true);
                    }
                    else
                    {
                        await DisplayAlert(AppResources.LoginFailed, AppResources.IncorrectCredential, AppResources.OK);
                        IsBusy = false;
                    }


                }
                else
                {
                    IsBusy = false;
                    await DisplayAlert(AppResources.UsernameAndPasswordRequired, AppResources.PleaseCheckInternatConnection, AppResources.OK);
                }


            };
        }

        void OnTappedForgetPassword(object sender, EventArgs args)
        {
            var forgetPasswordPage = new ForgetPasswordPage();
            Navigation.PushAsync(forgetPasswordPage);
        }

        void OnTappedSignUp(object sender, EventArgs args)
        {
            String url = Constants.host + "/guest/register";
            Device.OpenUri(new Uri(url));
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            this.BackgroundImage = null;
            GC.Collect();
        }
    }


}
