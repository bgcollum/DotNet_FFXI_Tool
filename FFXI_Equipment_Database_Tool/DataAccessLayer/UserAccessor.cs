using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataObjects;
using DataAccessLayerInterfaces;
using System.Data.SqlClient;
using System.Data;

namespace DataAccessLayer
{
    public class UserAccessor : IUserAccessor
    {
        public int AuthenticateUserByEmailAndPasswordHash(string email, string passwordhash)
        {
            // stored procedure returns success or failure as boolean
            int result = 0; // Set default to failure

            // ADO.NET needs a connection
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            // Command tet
            var cmdText = "sp_authenticate_user";   // Stored procedure
            // Build command object
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            // Parameters
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 100);
            cmd.Parameters["@Email"].Value = email;
            cmd.Parameters["@PasswordHash"].Value = passwordhash;
            // Run the stored procedure
            try
            {
                conn.Open();
                // cmd.ExecuteScalar() runs the SP which returns 0 or 1 which must be converted to an Int
                result = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }
            // Boolean result is now populated
            return result;
        }
        public List<string> SelectAllUserRoles()
        {
            List<string> listOfRoles = new List<string>();

            // ADO.NET needs a connection
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            // Command tet
            var cmdText = "sp_select_all_user_roles";   // Stored procedure
            // Build command object
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            // No Parameters
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string userRole = reader.GetString(0);
                        listOfRoles.Add(userRole);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            if(listOfRoles.Count == 0)
            {
                listOfRoles.Add("Failed to retrieve user roles from database");   // Dummy entry to ensure list always has at least one value
            }
            return listOfRoles;
        }

        public List<string> SelectRolesByUserID(int userID)
        {
            // Instantiate role list
            List<string> userRoles = new List<string>();

            // ADO.NET needs a connection
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            // Command tet
            var cmdText = "sp_select_roles_by_userID";   // Stored procedure
            // Build command object
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            // Parameters
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters["@UserID"].Value = userID;
            // Run the stored procedure
            try
            {
                conn.Open();
                // Execute command
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        userRoles.Add(reader.GetString(0));
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }
            // Boolean result is now populated
            return userRoles;
        }

        public User SelectUserByEmail(string email)
        {
            // Initialize user object
            User user = null;

            // Connection
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            // Command text
            var cmdText = "sp_select_user_by_email";
            // Command
            var cmd = new SqlCommand(cmdText, conn);
            // Command type
            cmd.CommandType = CommandType.StoredProcedure;
            // Paramemters
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
            // Value
            cmd.Parameters["@Email"].Value = email;
            // Try-catch-finally
            try
            {
                // open
                conn.Open();
                // Execute and get a SqlDataReader
                var reader = cmd.ExecuteReader();

                if (reader.HasRows) // Will be true when there is anything to read
                {
                    reader.Read(); // Read the first row
                    // [UserID], [UserName], [UserEmail]
                    user = new User();
                    // user.UserID = reader.GetInt32(0);
                    user.UserID = Convert.ToInt32(reader["UserID"]);
                    // user.UserName = reader.GetString(1);
                    user.UserName = Convert.ToString(reader["User_Name"]);
                    // user.UserEmail = reader.GetString(2);
                    user.UserEmail = Convert.ToString(reader["Email"]);
                }
                else
                {
                    throw new ApplicationException("User not found.");
                }
                // Close the reader
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Close
                conn.Close();
            }

            return user;
        }

        public int UpdatePasswordHash(int userID, string newPasswordHash, string oldPasswordHash)
        {
            {
                // Same logical structure as above
                // stored procedure returns success or failure as int (1 or 0)
                int result = 0; // Set default to failure

                // ADO.NET needs a connection
                DBConnection connectionFactory = new DBConnection();
                var conn = connectionFactory.GetConnection();
                // Command tet
                var cmdText = "sp_update_passwordHash";   // Stored procedure
                // Build command object
                var cmd = new SqlCommand(cmdText, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                // Parameters
                cmd.Parameters.Add("@UserID", SqlDbType.Int);
                cmd.Parameters.Add("@NewPasswordHash", SqlDbType.NVarChar, 100);
                cmd.Parameters.Add("@OldPasswordHash", SqlDbType.NVarChar, 100);
                cmd.Parameters["@UserID"].Value = userID;
                cmd.Parameters["@NewPasswordHash"].Value = newPasswordHash;
                cmd.Parameters["@OldPasswordHash"].Value = oldPasswordHash;
                // Run the stored procedure
                try
                {
                    conn.Open();
                    // ExecuteNonQuery directly returns rows affected
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
                // Boolean result is now populated
                return result;
            }
        }
        public int InsertNewUser(User user) // Based off Jim's BicycleDB
        {
            int userID = 0;
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_insert_new_user";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@User_Name", user.UserName);
            cmd.Parameters.AddWithValue("@Email", user.UserEmail);
            try
            {
                conn.Open();
                userID = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return userID;
        }
        public int AddOrRemoveRoleFromUser(int userID, string role, bool delete = false)    // Based off Jim's BicycleDB
        {
            int rows = 0;
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            // Checks the local boolean 'delete' passed as an argument to select which text is assigned to cmdText
            var cmdText = delete ? "sp_remove_role_from_user" : "sp_add_role_to_user";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID", userID);
            cmd.Parameters.AddWithValue("@RoleID", role);
            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }
    }
}
