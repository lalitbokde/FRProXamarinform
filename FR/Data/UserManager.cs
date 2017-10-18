using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FR
{
    public class UserManager
    {
        IUserRestService restService;

        public UserManager(IUserRestService service)
        {
            restService = service;
        }

        public Task<UserResponseResult> Authentication(Dictionary<string, string> credential)
        {
            return restService.Authentication(credential);
        }

        public Task<User> GetUser(int userID)
        {
            return restService.GetUser(userID);
        }

        public Task<List<User>> SearchUsers(String keywords)
        {
            return restService.SearchUsers(keywords);
        }

        public Task<User> UploadProfileImage(MediaFile file, int userID)
        {
            return restService.UploadProfileImage(file, userID);
        }

    }
}
