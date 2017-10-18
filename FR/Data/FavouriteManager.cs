using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FR
{
    public class FavouriteManager
    {
        IFavouriteRestService restService;

        public FavouriteManager(IFavouriteRestService service)
        {
            restService = service;
        }

        public Task<ObservableCollection<Favourite>> GetByUser(int userID)
        {
            return restService.GetByUser(userID);
        }

        public Task<Favourite> Create(int userID, int chatMessageID)
        {
            return restService.Create(userID, chatMessageID);
        }

        public Task Delete(int favouriteID)
        {
            return restService.Delete(favouriteID);
        }
    }
}
