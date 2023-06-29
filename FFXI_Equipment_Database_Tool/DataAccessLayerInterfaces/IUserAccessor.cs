using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataObjects;

namespace DataAccessLayerInterfaces
{
    public interface IUserAccessor
    {
        // Controls for user login

        // Authenticate User (Int functions as boolean for success)
        int AuthenticateUserByEmailAndPasswordHash(string email, string passwordhash);

        // Select User
        User SelectUserByEmail(string email);

        // Get list of user roles
        List<String> SelectAllUserRoles();

        // Role selection
        List<string> SelectRolesByUserID(int userID);

        // Update Password Hash
        int UpdatePasswordHash(int userID, string newPasswordHash, string oldPasswordHash);

        // Add new user
        int InsertNewUser(User user);

        // Add or remove a role from a user
        int AddOrRemoveRoleFromUser(int userID, string role, bool delete = false);
    }
}
