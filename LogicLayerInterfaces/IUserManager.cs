using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataObjects;

namespace LogicLayerInterfaces
{
    public interface IUserManager
    {
        // Actual password from presentation layer

        // Log in event must return a user or throw exception
        User LoginUser(string userEmail, string userPassword);
        // Need to be able to compare userPassword to hash value
        string HashSHA256(string source);
        bool ResetPassword(User user, string userEmail, string newUserPassword, string oldUserPassword);
        List<String> RetrieveUserRoles();
        // FindUser method added 4/4/2023
        bool FindUser(string email);
        // RetrieveUserIDFromEmail method added 2023/04/06/
        User RetrieveUserFromEmail(string email);
        // RetrieveUserIDFromEmail method added 2023/04/06/
        int RetrieveUserIDFromEmail(string email);
        // Add User
        bool AddUser(User user);
        // Add Role to user
        bool AddRoleToUser(User user, string role);
        // Remove Role from user
        bool RemoveRoleFromUser(User user, string role);
    }
}
