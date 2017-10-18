using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FR
{
    public interface IRelationshipRestService
    {
        Task<List<Relationship>> GetFriends(int userID);

        Task<List<Relationship>> GetNewFriendsRequest(int userID);

        Task FriendRequest(Relationship relationship);

        Task RequestApprove(Friend relationship);

        Task RequestDecline(Friend relationship);

        Task FriendBlock(Friend relationship);

    }
}
