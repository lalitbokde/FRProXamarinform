using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FR
{
    public interface IFavouriteRestService
    {
        Task<ObservableCollection<Favourite>> GetByUser(int userID);

        Task<Favourite> Create(int userID, int chatMessageID);

        Task Delete(int favouriteID);
    }
}
