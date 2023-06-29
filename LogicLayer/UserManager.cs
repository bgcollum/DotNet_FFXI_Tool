using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LogicLayerInterfaces;
using DataAccessLayerInterfaces;
using DataAccessLayer;
using System.Security.Cryptography; // For Password hashing
using DataObjects;

namespace LogicLayer
{
    public class UserManager : IUserManager
    {
        // Instantiate object to access Data Access Layer Interface
        private IUserAccessor _userAcessor = null;
        public UserManager()    // Default constructor
        {
            _userAcessor = new UserAccessor();
        }
        public UserManager(IUserAccessor iUserAccessor) // Take a specific IUserAccessor interface
        {
            _userAcessor = iUserAccessor;
        }

        public bool AddRoleToUser(User user, string role)
        {
            bool result = false;
            try
            {
                result = (1 == _userAcessor.AddOrRemoveRoleFromUser(user.UserID, role));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to add role to user.", ex);
            }
            return result;
        }
        public bool RemoveRoleFromUser(User user, string role)
        {
            bool result = false;
            try
            {
                result = (1 == _userAcessor.AddOrRemoveRoleFromUser(user.UserID, role, delete: true));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to remove role from user.", ex);
            }
            return result;
        }

        public bool AddUser(User user)
        {
            bool result = true;
            try
            {
                result = _userAcessor.InsertNewUser(user) > 0;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("User not added.", ex);
            }
            return result;
        }

        public bool FindUser(string email)
        {
            try
            {
                // Interesting format to return a boolean here
                return _userAcessor.SelectUserByEmail(email) != null;
            }
            catch (ApplicationException exOne)
            {
                if (exOne.Message == "User not found.")  // See_userAcessor.SelectUserByEmail(email) special Exception
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
            catch (Exception exTwo)
            {
                throw new ApplicationException("Database error", exTwo);
            }
        }

        public string HashSHA256(string source) // Take a string source, return HashSHA256 value for it
        {
            // Still using 'newuser' hash for now
            // '9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e'

            string result = "";
            if (source == "" || source == null)
            {
                throw new ArgumentNullException("No value passed, nothing to hash");
            }
            // Create a Byte Array
            byte[] dataArray;
            // Creating a .NET hash provider object
            // Using statement makes this hasher object function local (Fresh seeds?)
            using (SHA256 sha256Hasher = SHA256.Create())
            {
                // hash the source string, with UTF8 formatting
                dataArray = sha256Hasher.ComputeHash(Encoding.UTF8.GetBytes(source));
            }
            // Creating output string with a StringBuilder object
            var sBuilder = new StringBuilder();
            // Loop through the hashed output creating characters from the values in the byte array
            for (int i = 0; i < dataArray.Length; i++)
            {
                sBuilder.Append(dataArray[i].ToString("x2"));   // x2 argument is for Hexadecimal
            }
            // Convert StringBuilder to an actual string. ToLower() isn't strictly necessary
            result = sBuilder.ToString().ToLower();
            return result;
        }

        public User LoginUser(string userEmail, string userPassword)
        {
            // Instantiate User object
            User user = null;
            try
            {
                userPassword = HashSHA256(userPassword);    // Hash the password
                // Check the hashed password versus the password hash stored on the database
                // This stored procedure returns 0 for fail and 1 for success
                if (_userAcessor.AuthenticateUserByEmailAndPasswordHash(userEmail, userPassword) == 1)
                {
                    // Load the user
                    user = _userAcessor.SelectUserByEmail(userEmail);
                    // Now that user is loaded roles can be added
                    user.UserRoles = _userAcessor.SelectRolesByUserID(user.UserID);
                }
                else
                {
                    throw new ApplicationException("User not found.");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Invalid User Name or Password.", ex);
            }
            return user;
        }
        public bool ResetPassword(User user, string userEmail, string newUserPassword, string oldUserPassword)
        {
            bool passwordChanged = false;
            // Hash the new and old passwords provided by the user
            newUserPassword = HashSHA256(newUserPassword);
            oldUserPassword = HashSHA256(oldUserPassword);
            if (user.UserEmail != userEmail)
            {
                passwordChanged = false;    // Short circuit on bad password
            }
            // Again, should either return 0 or 1 for failure or success, respectively
            else if (_userAcessor.UpdatePasswordHash(user.UserID, newUserPassword, oldUserPassword) == 1)
            {
                passwordChanged = true; // User password actually updated above, return confirmation of success
            }
            return passwordChanged;
        }
        public int RetrieveUserIDFromEmail(string email)
        {
            try
            {
                return _userAcessor.SelectUserByEmail(email).UserID;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Database Error", ex);
            }
        }
        public User RetrieveUserFromEmail(string email)
        {
            try
            {
                return _userAcessor.SelectUserByEmail(email);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Database Error", ex);
            }
        }
        public List<string> RetrieveUserRoles()
        {
            List<string> listOfUserRoles = new List<string>();
            try
            {
                listOfUserRoles = _userAcessor.SelectAllUserRoles();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to retrieve list of user roles from DAL. " + ex.Message, ex);
            }
            return listOfUserRoles;
        }
    }
}
