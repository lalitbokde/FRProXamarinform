using FR.Resx;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FR.ViewModels
{
    public class EnrollItemViewModel : BaseViewModel
    {
        private readonly EnrollListViewModel enrollListViewModel;
        public Enroll enroll { get; set; }

        private bool isUpLoadImageExist;
        public bool IsUploadImageExist
        {
            get { return isUpLoadImageExist; }
            set { SetField(ref isUpLoadImageExist, value); }
        }

        private bool isUpLoadAvailable;
        public bool IsUploadAvailable
        {
            get { return isUpLoadAvailable; }
            set { SetField(ref isUpLoadAvailable, value); }
        }

        ICommand tapUploadCommand;
        public ICommand TapUploadCommand
        {
            get { return tapUploadCommand; }
        }

        ICommand tapUpdateCommand;
        public ICommand TapUpdateCommand
        {
            get { return tapUpdateCommand; }
        }

        ICommand tapDeleteCommand;
        public ICommand TapDeleteCommand
        {
            get { return tapDeleteCommand; }
        }

        public EnrollItemViewModel(EnrollListViewModel enrollListViewModel)
        {
            tapUploadCommand = new Command(OnTappedUpload);
            tapUpdateCommand = new Command(OnTappedUpdate);
            tapDeleteCommand = new Command(OnTappedDelete);

            if (enrollListViewModel == null)
            {
                throw new ArgumentNullException("EnrollListViewModel");
            }
            this.enrollListViewModel = enrollListViewModel;

        }

        async void OnTappedUpload(object parameter)
        {
            int enrollID = (int)parameter;

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                MessagingCenter.Send(this, "Info", AppResources.PhotosNotSupported);
                return;
            }
            var file = await CrossMedia.Current.PickPhotoAsync();


            if (file == null)
                return;

            MessagingCenter.Send(this, "Loading");
            ResponseResult responseResult = await App.EnrollManager.Upload(enrollID, file);
            MessagingCenter.Send(this, "FinishLoading");

            if (responseResult.Success == true)
            {
                MessagingCenter.Send(this, "Upload", responseResult.Message);
            }
            else
            {
                MessagingCenter.Send(this, "info", responseResult.Error);
            }
        }

        async void OnTappedUpdate(object parameter)
        {
            int enrollID = (int)parameter;

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                MessagingCenter.Send(this, "Info", AppResources.PhotosNotSupported);
                return;
            }
            var file = await CrossMedia.Current.PickPhotoAsync();


            if (file == null)
                return;

            MessagingCenter.Send(this, "Loading");
            ResponseResult responseResult = await App.EnrollManager.Update(enrollID, file);
            MessagingCenter.Send(this, "FinishLoading");

            if (responseResult.Success == true)
            {
                MessagingCenter.Send(this, "Update", responseResult.Message);
            }
            else
            {
                MessagingCenter.Send(this, "info", responseResult.Error);
            }
        }

        async void OnTappedDelete(object parameter)
        {
            int enrollID = (int)parameter;

            MessagingCenter.Send(this, "Loading");
            ResponseResult responseResult = await App.EnrollManager.Delete(enrollID);
            MessagingCenter.Send(this, "FinishLoading");

            if (responseResult.Success == true)
            {
                MessagingCenter.Send(this, "Delete", responseResult.Message);
            }
            else
            {
                MessagingCenter.Send(this, "info", responseResult.Error);
            }
        }
    }
}
