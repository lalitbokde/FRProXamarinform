using System.Collections.Generic;
using System.Threading.Tasks;

namespace FR
{
    public class ChatRoomItemManager
    {
        IChatRoomRestService restService;

        public ChatRoomItemManager(IChatRoomRestService service)
        {
            restService = service;
        }

        public Task<List<ChatRoom>> GetTasksAsync(int categoryId)
        {
            return restService.RefreshDataAsync(categoryId);
        }
    }
}
