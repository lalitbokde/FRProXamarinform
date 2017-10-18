using System.Collections.Generic;
using System.Threading.Tasks;

namespace FR
{
    public class RelationshipManager
    {
        IRelationshipRestService restService;

        public RelationshipManager(IRelationshipRestService service)
        {
            restService = service;
        }

        public Task<List<Relationship>> GetFriends(int userID)
        {
            return restService.GetFriends(userID);
        }

        public Task<List<Relationship>> GetNewFriendsRequest(int userID)
        {
            return restService.GetNewFriendsRequest(userID);
        }

        public Task FriendRequest(Relationship relationship)
        {
            return restService.FriendRequest(relationship);
        }

        public Task RequestApprove(Friend relationship)
        {
            return restService.RequestApprove(relationship);
        }

        public Task RequestDecline(Friend relationship)
        {
            return restService.RequestDecline(relationship);
        }

        public Task FriendBlock(Friend relationship)
        {
            return restService.FriendBlock(relationship);
        }
    }
}
