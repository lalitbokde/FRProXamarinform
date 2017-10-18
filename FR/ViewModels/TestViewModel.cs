using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FR.ViewModels
{
    public class TestViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<string> Items { get; set; }
        Page page;
        public TestViewModel(Page page)
        {
            this.page = page;
            Items = new ObservableCollection<string>();
        }

        bool isBusy;

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                if (isBusy == value)
                    return;

                isBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }

        ICommand refreshCommand;

        public ICommand RefreshCommand
        {
            get { return refreshCommand ?? (refreshCommand = new Command(async () => await ExecuteRefreshCommand())); }
        }

        async Task ExecuteRefreshCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            Items.Clear();

            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {

                for (int i = 0; i < 100; i++)
                    Items.Add(DateTime.Now.AddMinutes(i).ToString("F"));

                IsBusy = false;

                page.DisplayAlert("Refreshed", "You just refreshed the page! Nice job!", "OK");
                return false;
            });
        }

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
