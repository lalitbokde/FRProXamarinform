using Newtonsoft.Json;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

#if __ANDROID__
using Android.Media;				
#endif

namespace FR
{
    public class EnrollRestService : IEnrollRestService
    {
        HttpClient client;
        public ObservableCollection<Enroll> Items { get; private set; }

        public EnrollRestService()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            client.Timeout = TimeSpan.FromMinutes(5);
        }

        async Task<EnrollResponseResult> IEnrollRestService.Create(int senderUserID, string name, string telephone, string remark, MediaFile file)
        {
            EnrollResponseResult rr = new EnrollResponseResult();
            var url = Constants.host + "/api/enroll/create";
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
                StringContent scSenderUserID = new StringContent(senderUserID.ToString());
                StringContent scName = new StringContent(name.ToString());
                StringContent scTelephone = new StringContent(telephone.ToString());
                StringContent scRemark = new StringContent(remark.ToString());
                StringContent scUploadImage = new StringContent(base64String);
                StringContent scImageOrientation = new StringContent(rotate.ToString());
                content.Add(scSenderUserID, "sender_user_id");
                content.Add(scName, "name");
                content.Add(scTelephone, "telephone");
                content.Add(scRemark, "remark");
                content.Add(scUploadImage, "str_image");
                content.Add(scImageOrientation, "img_orientation");

                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    rr = JsonConvert.DeserializeObject<EnrollResponseResult>(result);

                    Debug.WriteLine(@"				Enroll insert successfully.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return rr;
        }

        async Task<EnrollListResponseResult> IEnrollRestService.GetAllByPage(int pageNo, int totalItemPerPage)
        {
            EnrollListResponseResult rr = new EnrollListResponseResult();
            //Items = new ObservableCollection<Enroll>();
            var url = Constants.host + "/api/enroll/getallbypage";
            var uri = new Uri(url);

            try
            {
                var postData = new List<KeyValuePair<string, string>>();
                postData.Add(new KeyValuePair<string, string>("page_no", pageNo.ToString()));
                postData.Add(new KeyValuePair<string, string>("total_item_per_page", totalItemPerPage.ToString()));
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
                    rr = JsonConvert.DeserializeObject<EnrollListResponseResult>(result, settings);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return rr;
        }

        async Task<ResponseResult> IEnrollRestService.Delete(int enrollID)
        {
            ResponseResult rr = new ResponseResult();
            var url = Constants.host + "/api/enroll/deleteenrollimage";
            var uri = new Uri(url);

            try
            {

                var postData = new List<KeyValuePair<string, string>>();
                postData.Add(new KeyValuePair<string, string>("enroll_id", enrollID.ToString()));
                HttpContent content = new FormUrlEncodedContent(postData);

                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    rr = JsonConvert.DeserializeObject<ResponseResult>(result);

                    Debug.WriteLine(@"				Enroll image delete successfully.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return rr;
        }

        async Task<ResponseResult> IEnrollRestService.Upload(int enrollID, MediaFile file)
        {
            ResponseResult rr = new ResponseResult();
            var url = Constants.host + "/api/enroll/uploadenrollimage";
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
                StringContent scEnrollID = new StringContent(enrollID.ToString());
                StringContent scUploadImage = new StringContent(base64String);
                StringContent scImageOrientation = new StringContent(rotate.ToString());
                content.Add(scEnrollID, "enroll_id");
                content.Add(scUploadImage, "str_image");
                content.Add(scImageOrientation, "img_orientation");

                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    rr = JsonConvert.DeserializeObject<ResponseResult>(result);

                    Debug.WriteLine(@"				Enroll upload image successfully.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return rr;
        }

        async Task<ResponseResult> IEnrollRestService.Update(int enrollID, MediaFile file)
        {
            ResponseResult rr = new ResponseResult();
            var url = Constants.host + "/api/enroll/updateenrollimage";
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
                StringContent scEnrollID = new StringContent(enrollID.ToString());
                StringContent scUploadImage = new StringContent(base64String);
                StringContent scImageOrientation = new StringContent(rotate.ToString());
                content.Add(scEnrollID, "enroll_id");
                content.Add(scUploadImage, "str_image");
                content.Add(scImageOrientation, "img_orientation");

                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    rr = JsonConvert.DeserializeObject<ResponseResult>(result);

                    Debug.WriteLine(@"				Enroll update image successfully.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return rr;
        }
    }
}
