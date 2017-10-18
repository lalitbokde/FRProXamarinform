using FR.Resx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace FR.Component
{
    public partial class BankInfo : StackLayout
    {
        public string BankName
        {
            get { return lblBankName.Text; }
            set { lblBankName.Text = value; }
        }

        public string BankCardNumber
        {
            get { return lblBankCardNumber.Text; }
            set { lblBankCardNumber.Text = value; }
        }

        public BankInfo()
        {
            InitializeComponent();
            lblBankNameLabel.Text = String.Format("{0} : ", AppResources.BankName);
            lblBankCardNumberLabel.Text = String.Format("{0} : ", AppResources.BankCardNumber);
        }
    }
}
