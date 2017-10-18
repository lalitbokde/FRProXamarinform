using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace FR
{
    public partial class TestFavouriteAPIPage : ContentPage
    {
        public TestFavouriteAPIPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void OnClicked(object sender, EventArgs e)
        {

            //pls change base on your database data
            int userId = 2;
            int chatMessageID = 264;
            int newfavouriteID = 0;

            //create new favaurite
            Favourite newFavourite = await App.FavouriteManager.Create(userId, chatMessageID);
            newfavouriteID = newFavourite.ID;

            //delete favourite
            await App.FavouriteManager.Delete(newfavouriteID);

            //get original count
            int count = 0;
            ObservableCollection<Favourite> favouriteList = await App.FavouriteManager.GetByUser(userId);
            count = favouriteList.Count;

            //create a few new favourite
            Favourite newFavourite1 = await App.FavouriteManager.Create(userId, chatMessageID);
            Favourite newFavourite2 = await App.FavouriteManager.Create(userId, chatMessageID);

            //get favourite list by user
            int newCount = 0;
            favouriteList = await App.FavouriteManager.GetByUser(userId);
            newCount = favouriteList.Count;

            if (count + 2 == newCount)
            {
                Debug.WriteLine(@"				Count Correct!!");
            }
        }
    }
}
