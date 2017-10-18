using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using Plugin.Media.Abstractions;

#if __ANDROID__
using Android.Media;				
#endif

namespace FR
{
    public class UserRestService : IUserRestService
    {
        HttpClient client;
        public List<User> Items { get; private set; }

        public UserRestService()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            client.Timeout = TimeSpan.FromMinutes(5);
        }

        async Task<UserResponseResult> IUserRestService.Authentication(Dictionary<string, string> credential)
        {
            UserResponseResult userResponseResult = new UserResponseResult();

            Credential userInfo = new Credential();
            userInfo.username = credential["Username"];
            userInfo.password = credential["Password"];

            var url = Constants.host + "/api/user/authentication";
            var uri = new Uri(url);

            try
            {
                var json = JsonConvert.SerializeObject(userInfo);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    userResponseResult = JsonConvert.DeserializeObject<UserResponseResult>(result);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return userResponseResult;
        }

        async Task<User> IUserRestService.GetUser(int userID)
        {
            User user = new User();

            var url = Constants.host + "/api/user/getuser";
            var uri = new Uri(url);

            try
            {
                var json = JsonConvert.SerializeObject(new { user_id = userID });
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    };
                    user = JsonConvert.DeserializeObject<User>(result, settings);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return user;
        }

        async Task<List<User>> IUserRestService.SearchUsers(String strKeywords)
        {
            Items = new List<User>();
            var url = Constants.host + "/api/user/searchusers";
            var uri = new Uri(url);

            try
            {
                var json = JsonConvert.SerializeObject(new { keywords = strKeywords, user_id = App.mUser.ID });
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    };
                    Items = JsonConvert.DeserializeObject<List<User>>(result, settings);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return Items;
        }

        async Task<User> IUserRestService.UploadProfileImage(MediaFile file, int userID)
        {
            //variable
            User user = new User();
            var url = Constants.host + "/api/user/uploadavatar";
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

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(upfilebytes);

                //create new HttpClient and MultipartFormDataContent and add our file
                MultipartFormDataContent content = new MultipartFormDataContent();
                StringContent userIdContent = new StringContent(userID.ToString());
                StringContent uploadImageContent = new StringContent(base64String);
                StringContent imageOrientationContent = new StringContent(rotate.ToString());
                content.Add(userIdContent, "user_id");
                content.Add(uploadImageContent, "str_image");
                content.Add(imageOrientationContent, "img_orientation");


                //upload MultipartFormDataContent content async and store response in response var
                var response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    };
                    user = JsonConvert.DeserializeObject<User>(result, settings);
                }
            }
            catch (Exception e)
            {
                //debug
                Debug.WriteLine("Exception Caught: " + e.ToString());
            }

            return user;
        }

    }
}
