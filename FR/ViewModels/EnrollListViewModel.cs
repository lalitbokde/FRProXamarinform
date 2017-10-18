using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;
using static FR.GenericUtilities.StringUtils;

namespace FR.ViewModels
{
    public class EnrollListViewModel : BaseViewModel
    {
        private ObservableCollection<EnrollItemViewModel> enrolls;
        public ObservableCollection<EnrollItemViewModel> Enrolls
        {
            get { return enrolls; }
            set { SetField(ref enrolls, value); }
        }

        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }

        public EnrollListViewModel(EnrollListResponseResult enrollListResponseResult, int currentPage)
        {
            enrolls = new ObservableCollection<EnrollItemViewModel>();
            TotalPage = enrollListResponseResult.TotalPage;
            CurrentPage = currentPage;

            List<char> filter = new List<char>();
            char space = ' ';
            filter.Add(space);

            foreach (Enroll enroll in enrollListResponseResult.Enrolls)
            {
                EnrollItemViewModel enrollItemViewModel = new EnrollItemViewModel(this);
                enroll.Telephone = "( " + HidePhoneNo(enroll.Telephone, 'X') + " )";
                if (enroll.File != "")
                {
                    enroll.File = Constants.host + "/" + enroll.File;
                }
                enrollItemViewModel.enroll = enroll;

                if (enroll.File == "")
                {
                    enrollItemViewModel.IsUploadImageExist = false;
                }
                else
                {
                    enrollItemViewModel.IsUploadImageExist = true;
                }

                if (enroll.Sender_User_ID == App.mUser.ID && enroll.File == "")
                {
                    enrollItemViewModel.IsUploadAvailable = true;
                }
                else
                {
                    enrollItemViewModel.IsUploadAvailable = false;
                }

                enrolls.Add(enrollItemViewModel);

            }
        }
    }
}
