using Plugin.Media.Abstractions;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace FR
{
    public class EnrollManager
    {
        IEnrollRestService restService;

        public EnrollManager(IEnrollRestService service)
        {
            restService = service;
        }

        public Task<EnrollListResponseResult> GetAllByPage(int pageNo, int totalItemPerPage)
        {
            return restService.GetAllByPage(pageNo, totalItemPerPage);
        }

        public Task<EnrollResponseResult> Create(int senderUserID, string name, string telephone, string remark, MediaFile file)
        {
            return restService.Create(senderUserID, name, telephone, remark, file);
        }

        public Task<ResponseResult> Delete(int enrollID)
        {
            return restService.Delete(enrollID);
        }

        public Task<ResponseResult> Upload(int enrollID, MediaFile file)
        {
            return restService.Upload(enrollID, file);
        }

        public Task<ResponseResult> Update(int enrollID, MediaFile file)
        {
            return restService.Update(enrollID, file);
        }
    }
}
