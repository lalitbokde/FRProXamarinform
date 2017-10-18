using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Media.Abstractions;
using System.IO;
using System.Collections.ObjectModel;

#if __ANDROID__
using Android.Media;				
#endif
namespace FR
{
    public class ChatMessageRestService : IChatMessageRestService
    {
        HttpClient client;
        public ObservableCollection<ChatMessage> Items { get; private set; }

        public ChatMessageRestService()
        {
            //var authData = string.Format("{0}:{1}", Constants.Username, Constants.Password);
            //var authHeaderValue = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(authData));

            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            client.Timeout = TimeSpan.FromMinutes(5);
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
        }

        async Task<ObservableCollection<ChatMessage>> IChatMessageRestService.GetAll(int chatRoomID)
        {
            Items = new ObservableCollection<ChatMessage>();

            // http://192.168.1.8/chatmessage/getall?chat_room_id=1
            var uri = new Uri(Constants.host + "/api/chatmessage/getall?chat_room_id=" + chatRoomID);

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
                    Items = JsonConvert.DeserializeObject<ObservableCollection<ChatMessage>>(content, settings);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return Items;
        }

        async Task<ChatMessageListResponseResult> IChatMessageRestService.GetResultPageByChatRoomID(int chatRoomID, int pageNumber, int itemPerPage, int lastDisplayHistoryChatMessageID)
        {
            ChatMessageListResponseResult rr = new ChatMessageListResponseResult();
            var uri = new Uri(Constants.host + "/api/chatmessage/getresultpage?chat_room_id=" + chatRoomID 
                + "&page_number=" + pageNumber 
                + "&item_per_page=" + itemPerPage
                + "&last_display_history_chat_message_id=" + lastDisplayHistoryChatMessageID);

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
                    rr = JsonConvert.DeserializeObject<ChatMessageListResponseResult>(content, settings);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return rr;
        }

        async Task<ChatMessage> IChatMessageRestService.Create(ChatMessage chatMessage)
        {
            ChatMessageResponseResult rr = new ChatMessageResponseResult();
            var url = Constants.host + "/api/chatmessage/create";
            var uri = new Uri(url);

            try
            {
                var json = JsonConvert.SerializeObject(chatMessage);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    rr = JsonConvert.DeserializeObject<ChatMessageResponseResult>(result);

                    Debug.WriteLine(@"				Chat Message successfully saved.");
                }
               
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return rr.Result;
        }

        async Task<ObservableCollection<ChatMessage>> IChatMessageRestService.GetNewChatMessage(int chatRoomID, int requestUserID)
        {
            Items = new ObservableCollection<ChatMessage>();
            var uri = new Uri(Constants.host + "/api/chatmessage/getnewchatmessages?chat_room_id=" + chatRoomID + "&request_user_id=" + requestUserID);

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
                    Items = JsonConvert.DeserializeObject<ObservableCollection<ChatMessage>>(content, settings);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return Items;
        }

        async Task<ChatMessage> IChatMessageRestService.UploadImage(MediaFile file, int chatRoomID, int senderUserID)
        {
            ChatMessageResponseResult rr = new ChatMessageResponseResult();
            var url = Constants.host + "/api/chatmessage/uploadimage";
            var uri = new Uri(url);

            try
            {
                // Get Orientation
				int rotate = 0;

				#if __ANDROID__
								// Android-specific code

								var exifInterface = new ExifInterface(file.Path);
				                int orientation = exifInterface.GetAttributeInt(ExifInterface.TagOrientation, 0);
												switch (orientation)
								                {
								                    case (int)Orientation.Rotate270:
								                        rotate = 90;
								                        break;
								                    case (int)Orientation.Rotate180:
								                        rotate = 180;
								                        break;
								                    case (int)Orientation.Rotate90:
								                        rotate = 270;
								                        break;
								                }
				#endif

				//read file into upfilebytes array
				byte[] upfilebytes = null;
                using (var memoryStream = new MemoryStream())
                {
                    file.GetStream().CopyTo(memoryStream);
                    file.Dispose();
                    upfilebytes = memoryStream.ToArray();
                }

                Debug.WriteLine(@"				File Size: " + upfilebytes.Length); 

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(upfilebytes);

                //create new HttpClient and MultipartFormDataContent and add our file
                MultipartFormDataContent content = new MultipartFormDataContent();
                StringContent scChatRoomID = new StringContent(chatRoomID.ToString());
                StringContent scSenderUserID = new StringContent(senderUserID.ToString());
                StringContent scUploadImage = new StringContent(base64String);
                StringContent scImageOrientation = new StringContent(rotate.ToString());
                content.Add(scChatRoomID, "chat_room_id");
                content.Add(scSenderUserID, "sender_user_id");
                content.Add(scUploadImage, "str_image");
                content.Add(scImageOrientation, "img_orientation");

                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    };

                    rr = JsonConvert.DeserializeObject<ChatMessageResponseResult>(result);

                    Debug.WriteLine(@"				Chat Message Image successfully upload and saved.");

                    //user = JsonConvert.DeserializeObject<User>(result, settings);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return rr.Result;
        }

        async Task<ChatMessage> IChatMessageRestService.UploadVoice(byte[] audio, int chatRoomID, int senderUserID)
        {
            ChatMessageResponseResult rr = new ChatMessageResponseResult();
            var url = Constants.host + "/api/chatmessage/uploadvoice";
            var uri = new Uri(url);

            try
            {

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(audio);

                //create new HttpClient and MultipartFormDataContent and add our file
                MultipartFormDataContent content = new MultipartFormDataContent();
                StringContent scChatRoomID = new StringContent(chatRoomID.ToString());
                StringContent scSenderUserID = new StringContent(senderUserID.ToString());
                StringContent scUploadVoice = new StringContent(base64String);
                content.Add(scChatRoomID, "chat_room_id");
                content.Add(scSenderUserID, "sender_user_id");
                content.Add(scUploadVoice, "str_voice");

                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    };

                    rr = JsonConvert.DeserializeObject<ChatMessageResponseResult>(result);

                    Debug.WriteLine(@"				Chat Message Image successfully upload and saved.");

                    //user = JsonConvert.DeserializeObject<User>(result, settings);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return rr.Result;
        }

        async Task<ObservableCollection<ChatMessage>> IChatMessageRestService.GetAllPrivateMessages(int relationshipID)
        {
            Items = new ObservableCollection<ChatMessage>();

            // http://192.168.1.8/chatmessage/getall?chat_room_id=1
            var uri = new Uri(Constants.host + "/api/chatmessage/getallprivatemessages?id=" + relationshipID);

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
                    Items = JsonConvert.DeserializeObject<ObservableCollection<ChatMessage>>(content, settings);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return Items;
        }

        async Task<ChatMessageListResponseResult> IChatMessageRestService.GetPrivateMessagesResultPage(int relationshipID, int pageNumber, int itemPerPage, int lastDisplayHistoryChatMessageID)
        {
            ChatMessageListResponseResult rr = new ChatMessageListResponseResult();
            var uri = new Uri(Constants.host + "/api/chatmessage/getprivatemessagesresultpage?id=" + relationshipID
                + "&page_number=" + pageNumber
                + "&item_per_page=" + itemPerPage
                + "&last_display_history_chat_message_id=" + lastDisplayHistoryChatMessageID);

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
                    rr = JsonConvert.DeserializeObject<ChatMessageListResponseResult>(content, settings);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return rr;
        }

        async Task<ChatMessage> IChatMessageRestService.CreatePrivateMessage(ChatMessage chatMessage)
        {
            ChatMessageResponseResult rr = new ChatMessageResponseResult();
            var url = Constants.host + "/api/chatmessage/createprivatemessage";
            var uri = new Uri(url);

            try
            {
                var json = JsonConvert.SerializeObject(chatMessage);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    rr = JsonConvert.DeserializeObject<ChatMessageResponseResult>(result);

                    Debug.WriteLine(@"				Chat Message successfully saved.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return rr.Result;
        }

        async Task<ObservableCollection<ChatMessage>> IChatMessageRestService.GetNewPrivateMessages(int relationshipID, int requestUserID)
        {
            Items = new ObservableCollection<ChatMessage>();
            var uri = new Uri(Constants.host + "/api/chatmessage/getnewprivatemessages?id=" + relationshipID + "&request_user_id=" + requestUserID);

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
                    Items = JsonConvert.DeserializeObject<ObservableCollection<ChatMessage>>(content, settings);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return Items;
        }

        async Task<ChatMessage> IChatMessageRestService.UploadPMImage(MediaFile file, int senderUserID, int receiverUserID)
        {
            ChatMessageResponseResult rr = new ChatMessageResponseResult();
            var url = Constants.host + "/api/chatmessage/uploadpmimage";
            var uri = new Uri(url);

            try
            {
                // Get Orientation
				int rotate = 0;

#if __ANDROID__
				// Android-specific code

				var exifInterface = new ExifInterface(file.Path);
                int orientation = exifInterface.GetAttributeInt(ExifInterface.TagOrientation, 0);
								switch (orientation)
				                {
				                    case (int)Orientation.Rotate270:
				                        rotate = 90;
				                        break;
				                    case (int)Orientation.Rotate180:
				                        rotate = 180;
				                        break;
				                    case (int)Orientation.Rotate90:
				                        rotate = 270;
				                        break;
				                }
#endif

				//read file into upfilebytes array
				byte[] upfilebytes = null;
                using (var memoryStream = new MemoryStream())
                {
                    file.GetStream().CopyTo(memoryStream);
                    file.Dispose();
                    upfilebytes = memoryStream.ToArray();
                }

                Debug.WriteLine(@"				File Size: " + upfilebytes.Length);

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(upfilebytes);

                //create new HttpClient and MultipartFormDataContent and add our file
                MultipartFormDataContent content = new MultipartFormDataContent();
                StringContent scSenderUserID = new StringContent(senderUserID.ToString());
                StringContent scReceiverUserID = new StringContent(receiverUserID.ToString());
                StringContent scUploadImage = new StringContent(base64String);
                StringContent scImageOrientation = new StringContent(rotate.ToString());
                content.Add(scSenderUserID, "sender_user_id");
                content.Add(scReceiverUserID, "receiver_user_id");
                content.Add(scUploadImage, "str_image");
                content.Add(scImageOrientation, "img_orientation");

                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    };

                    rr = JsonConvert.DeserializeObject<ChatMessageResponseResult>(result);

                    Debug.WriteLine(@"				PM Image successfully upload and saved.");

                    //user = JsonConvert.DeserializeObject<User>(result, settings);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return rr.Result;
        }

        async Task<ChatMessage> IChatMessageRestService.UploadPMVoice(byte[] audio, int senderUserID, int receiverUserID)
        {
            ChatMessageResponseResult rr = new ChatMessageResponseResult();
            var url = Constants.host + "/api/chatmessage/uploadpmvoice";
            var uri = new Uri(url);

            try
            {

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(audio);

                //create new HttpClient and MultipartFormDataContent and add our file
                MultipartFormDataContent content = new MultipartFormDataContent();
                StringContent scSenderUserID = new StringContent(senderUserID.ToString());
                StringContent scReceiverUserID = new StringContent(receiverUserID.ToString());
                StringContent scUploadVoice = new StringContent(base64String);
                content.Add(scSenderUserID, "sender_user_id");
                content.Add(scReceiverUserID, "receiver_user_id");
                content.Add(scUploadVoice, "str_voice");

                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    };

                    rr = JsonConvert.DeserializeObject<ChatMessageResponseResult>(result);

                    Debug.WriteLine(@"				PM Voice successfully upload and saved.");

                    //user = JsonConvert.DeserializeObject<User>(result, settings);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return rr.Result;
        }

    }
}
