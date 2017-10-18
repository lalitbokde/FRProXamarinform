using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace FR
{
    public partial class MenuPage : MasterDetailPage
    {
        
        public MenuPage()
        {
            InitializeComponent();
            var menuPage = new DrawerPage();
            menuPage.OnMenuSelect = (categoryPage) =>
            {
                Detail = new NavigationPage(categoryPage)
                {
                    BarBackgroundColor = Color.FromHex("#ffffff"),
                    BarTextColor = Color.Black
                };
                IsPresented = false;
            };
            Master = menuPage;

            Detail = new NavigationPage(new ChatRoomCategoryListPage())
            {
                BarBackgroundColor = Color.FromHex("#ffffff"),//your color here
                BarTextColor = Color.Black
            };
        }
    }
}
