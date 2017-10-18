using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FR
{
    public class ChatRoomRestService : IChatRoomRestService
    {
        HttpClient client;

        public List<ChatRoom> Items { get; private set; }

        public ChatRoomRestService()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }

        async Task<List<ChatRoom>> IChatRoomRestService.RefreshDataAsync(int categoryId)
        {
            Items = new List<ChatRoom>();
            var uri = new Uri(Constants.host + "/api/chatroom/getall?category_id=" + categoryId);

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<ChatRoom>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return Items;
        }

    }
}
