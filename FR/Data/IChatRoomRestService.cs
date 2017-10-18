using System.Collections.Generic;
using System.Threading.Tasks;

namespace FR
{
    public interface IChatRoomRestService
    {
        Task<List<ChatRoom>> RefreshDataAsync(int categoryId);
    }
}
