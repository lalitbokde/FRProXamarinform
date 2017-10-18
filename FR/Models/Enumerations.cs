using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FR
{
    public class Enumerations
    {
        public enum ChatRoomType
        {
            Public = 0,
            Private = 1,
            FRChat = 2,
        };

        public enum Role
        {
            NormalUser = 1,
            SuperUser = 2,
            VIPCPMUser = 3,
            NormalAdmin = 4,
            SuperAdmin = 5,
            Guest = 6,
        }

        public enum ChatRoomCategory
        {
            Common = 0,
            FRMember = 1,
        }
    }
}
