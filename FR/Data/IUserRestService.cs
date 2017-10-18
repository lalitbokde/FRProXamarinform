using Plugin.Media.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FR
{
    public interface IUserRestService
    {
        Task<UserResponseResult> Authentication(Dictionary<string, string> credential);

        Task <User> GetUser(int userID);

        Task <List<User>> SearchUsers(string keywords);

        Task <User> UploadProfileImage(MediaFile file, int userID);
    }
}
