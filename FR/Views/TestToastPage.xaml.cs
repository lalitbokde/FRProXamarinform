using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace FR
{
    public partial class TestToastPage : ContentPage
    {
        public TestToastPage()
        {
            InitializeComponent();
        }

        private void OnClicked(object sender, EventArgs e)
        {
            ToastConfig tc = new ToastConfig("Your message already copy to clipboard")
                .SetDuration(TimeSpan.FromSeconds(3));

            UserDialogs.Instance.Toast(tc);

            //UserDialogs ud = new UserDialogs();
            //ud.Toast(new ToastConfig(this.Message)
            //       //.SetMessageTextColor(System.Drawing.Color.FromHex(this.MessageTextColor))
            //       .SetDuration(TimeSpan.FromSeconds(3))
            //       .SetAction(x => x
            //           .SetText(this.ActionText)
            //           //.SetTextColor(new System.Drawing.Color.FromHex(this.ActionTextColor))
            //           .SetAction(() => dialogs.Alert("You clicked the primary button"))
            //       )
            //   )
        }
    }
}
