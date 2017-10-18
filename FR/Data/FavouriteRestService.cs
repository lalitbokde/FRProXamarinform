using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FR
{
    public class FavouriteRestService : IFavouriteRestService
    {
        HttpClient client;
        public ObservableCollection<Favourite> Items { get; private set; }

        public FavouriteRestService()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            client.Timeout = TimeSpan.FromMinutes(5);
        }

        async Task<Favourite> IFavouriteRestService.Create(int userID, int chatMessageID)
        {
            FavouriteResponseResult rr = new FavouriteResponseResult();
            var url = Constants.host + "/api/favourite/create";
            var uri = new Uri(url);

            try
            {
                var postData = new List<KeyValuePair<string, string>>();
                postData.Add(new KeyValuePair<string, string>("user_id", userID.ToString()));
                postData.Add(new KeyValuePair<string, string>("chat_message_id", chatMessageID.ToString()));
                HttpContent content = new FormUrlEncodedContent(postData);

                HttpResponseMessage response = null;
                response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    rr = JsonConvert.DeserializeObject<FavouriteResponseResult>(result);

                    Debug.WriteLine(@"				Favourite insert successfully.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return rr.Result;
        }

        async Task IFavouriteRestService.Delete(int favouriteID)
        {
            var url = Constants.host + "/api/favourite/delete";
            var uri = new Uri(url);

            try
            {
                var postData = new List<KeyValuePair<string, string>>();
                postData.Add(new KeyValuePair<string, string>("id", favouriteID.ToString()));
                HttpContent content = new FormUrlEncodedContent(postData);

                HttpResponseMessage response = null;
                response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"				Friend block successfully.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
        }

        async Task<ObservableCollection<Favourite>> IFavouriteRestService.GetByUser(int userID)
        {
            Items = new ObservableCollection<Favourite>();
            var url = Constants.host + "/api/favourite/getbyuser";
            var uri = new Uri(url);

            try
            {
                var postData = new List<KeyValuePair<string, string>>();
                postData.Add(new KeyValuePair<string, string>("user_id", userID.ToString()));
                HttpContent content = new FormUrlEncodedContent(postData);

                HttpResponseMessage response = null;
                response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    };
                    Items = JsonConvert.DeserializeObject<ObservableCollection<Favourite>>(result, settings);
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
