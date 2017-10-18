using Plugin.Media.Abstractions;
using System.Threading.Tasks;

namespace FR
{
    public interface IEnrollRestService
    {
        Task<EnrollListResponseResult> GetAllByPage(int pageNo, int totalItemPerPage);

        Task<EnrollResponseResult> Create(int senderUserID, string name, string telephone, string remark, MediaFile file);

        Task<ResponseResult> Delete(int enrollID);

        Task<ResponseResult> Upload(int enrollID, MediaFile file);

        Task<ResponseResult> Update(int enrollID, MediaFile file);
    }
}
