using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace FR
{
    public partial class ForgetPasswordPage : ContentPage
    {
        public ForgetPasswordPage()
        {
            InitializeComponent();

            btnConfrim.Clicked += (s, e) =>
            {
                Navigation.PopAsync(true);
            };
        }
    }
}
