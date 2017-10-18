using FR.Resx;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace FR
{
    public partial class AddEnrollPage : ContentPage
    {
        public MediaFile mfUploadImage { get; set; }

        public AddEnrollPage()
        {
            InitializeComponent();
            this.Title = AppResources.AddEnroll;

            edtRemark.Text = AppResources.Remark; 
            edtRemark.TextColor = Color.Gray;

            btnSubmit.Clicked += async (sender, args) =>
            {
                if (etyName.Text == "")
                {
                    await DisplayAlert(AppResources.Error, AppResources.NoUserName, AppResources.OK);
                    return;
                }

                if (etyTelephone.Text == "")
                {
                    await DisplayAlert(AppResources.Error, AppResources.NoTelephoneNumber, AppResources.OK);
                    return;
                }

                IsBusy = true;
                EnrollResponseResult enrollResponseResult = await App.EnrollManager.Create(
                    App.mUser.ID, 
                    etyName.Text.Trim(), 
                    etyTelephone.Text.Trim(), 
                    edtRemark.Text.Trim(),
                    mfUploadImage);
                IsBusy = false;

                if (enrollResponseResult.Success == true)
                {
                    await Navigation.PopAsync();

                }
                else
                {
                    await DisplayAlert(AppResources.Error, enrollResponseResult.Error, AppResources.OK);
                }
            };

        }

        private void edtRemark_Focused(object sender, FocusEventArgs e) //triggered when the user taps on the Editor to interact with it
        {
            if (edtRemark.Text.Equals(AppResources.Remark)) 
            {
                edtRemark.Text = "";
                edtRemark.TextColor = Color.Black;
            }
        }

        private void edtRemark_Unfocused(object sender, FocusEventArgs e) 
        {
            if (edtRemark.Text.Equals("")) 
            {
                edtRemark.Text = AppResources.Remark;
                edtRemark.TextColor = Color.Gray;
            }
        }

        async void OnTappedImgUploadPicture(object sender, EventArgs args)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert(AppResources.PhotosNotSupported, AppResources.PermissionNotGrantedToPhotos, AppResources.OK);
                return;
            }
            var file = await CrossMedia.Current.PickPhotoAsync();


            if (file == null)
                return;
            else
                mfUploadImage = file;
        }

    }
}
