using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using FR.ViewModels;

namespace FR
{
    public partial class TestPullToRefreshPage : ContentPage
    {
        public TestPullToRefreshPage()
        {
            InitializeComponent();
            BindingContext = new TestViewModel(this);
        }
    }
}
