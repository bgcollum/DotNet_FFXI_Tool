using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccessLayerInterfaces;
using DataObjects;

namespace DataAccessLayerFakes
{
    public class UserAccessorFakes : IUserAccessor
    {
        // This class wasn't present in .NET II, as such these didn't exist for testing
        public UserAccessorFakes()
        {

        }

        public int AddOrRemoveRoleFromUser(int userID, string role, bool delete = false)
        {
            throw new NotImplementedException();
        }

        public int AuthenticateUserByEmailAndPasswordHash(string email, string passwordhash)
        {
            throw new NotImplementedException();
        }

        public int InsertNewUser(User user)
        {
            throw new NotImplementedException();
        }

        public List<string> SelectAllUserRoles()
        {
            List<string> roleList = new List<string>();
            roleList.Add("Administrator");
            roleList.Add("Anonymous_User");
            roleList.Add("Contributor");
            roleList.Add("Registered_User");
            return roleList;
        }

        public List<string> SelectRolesByUserID(int userID)
        {
            throw new NotImplementedException();
        }

        public User SelectUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public int UpdatePasswordHash(int userID, string newPasswordHash, string oldPasswordHash)
        {
            throw new NotImplementedException();
        }
    }
}
