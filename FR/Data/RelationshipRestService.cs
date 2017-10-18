using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FR
{
    public class RelationshipRestService : IRelationshipRestService
    {
        HttpClient client;
        public List<Relationship> Items { get; private set; }

        public RelationshipRestService()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }

        async Task IRelationshipRestService.FriendBlock(Friend relationship)
        {
            var url = Constants.host + "/api/relationship/friendblock";
            var uri = new Uri(url);

            try
            {
                var json = JsonConvert.SerializeObject(relationship);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    //var result = await response.Content.ReadAsStringAsync();
                    //user = JsonConvert.DeserializeObject<User>(result);

                    Debug.WriteLine(@"				Friend block successfully.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
        }

        async Task IRelationshipRestService.FriendRequest(Relationship relationship)
        {
            var url = Constants.host + "/api/relationship/friendrequest";
            var uri = new Uri(url);

            try
            {
                var json = JsonConvert.SerializeObject(relationship);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    //var result = await response.Content.ReadAsStringAsync();
                    //user = JsonConvert.DeserializeObject<User>(result);

                    Debug.WriteLine(@"				Relationship successfully saved.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
        }

        async Task<List<Relationship>> IRelationshipRestService.GetFriends(int userID)
        {
            Items = new List<Relationship>();

            var uri = new Uri(Constants.host + "/api/relationship/getfriends?user_id=" + userID);

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    };
                    Items = JsonConvert.DeserializeObject<List<Relationship>>(content, settings);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return Items;
        }

        async Task<List<Relationship>> IRelationshipRestService.GetNewFriendsRequest(int userID)
        {
            Items = new List<Relationship>();

            var uri = new Uri(Constants.host + "/api/relationship/getnewfriendsrequest?user_id=" + userID);

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    };
                    Items = JsonConvert.DeserializeObject<List<Relationship>>(content, settings);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return Items;
        }

        async Task IRelationshipRestService.RequestApprove(Friend friend)
        {
            var url = Constants.host + "/api/relationship/requestapprove";
            var uri = new Uri(url);

            try
            {
                var json = JsonConvert.SerializeObject(friend);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    //var result = await response.Content.ReadAsStringAsync();
                    //user = JsonConvert.DeserializeObject<User>(result);

                    Debug.WriteLine(@"				Relationship successfully saved.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
        }

        async Task IRelationshipRestService.RequestDecline(Friend friend)
        {
            var url = Constants.host + "/api/relationship/requestdecline";
            var uri = new Uri(url);

            try
            {
                var json = JsonConvert.SerializeObject(friend);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    //var result = await response.Content.ReadAsStringAsync();
                    //user = JsonConvert.DeserializeObject<User>(result);

                    Debug.WriteLine(@"				Relationship successfully saved.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
        }
    }
}
