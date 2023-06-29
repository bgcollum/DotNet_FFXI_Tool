using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using DataAccessLayerInterfaces;
using DataObjects;

namespace DataAccessLayer
{
    public class EquipmentDBAccessor : IEquipmentDBAccessor
    {
        // Master List Accessors
        // This fetches the Master Stat List from the Database
        public List<string> RetrieveMasterStatList()
        {
            // Instantiate the List to hold the Master Stat List
            List<string> master_stat_list_builder = new List<string>();

            // Connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            // Command Tet
            var cmdText = "sp_select_stat";
            // Command
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            // No parameters, no values for parameters

            try
            {
                // Open the connection to the Database
                conn.Open();

                // Read in data
                var reader = cmd.ExecuteReader();   // Create a reader object to read DB lines
                if (reader.HasRows) // If the reader has rows, read them
                {
                    while (reader.Read())   // Read
                    {
                        master_stat_list_builder.Add(reader.GetString(0));
                    }
                }
                else
                {
                    throw new DataException("Database Read failed: Master Stat List appears to be empty.");
                }
                // Return the List now that it has been built
                return master_stat_list_builder;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                // Close the connection to the Database
                conn.Close();
            }
        }
        // This fetches the simplified Master Equipment List from the Database
        public List<Item> RetrieveMasterEquipmentList()
        {
            List<Item> master_equipment_list_builder = new List<Item>();
            // Connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            // Command Tet
            var cmdText = "sp_select_item_simple";
            // Command
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            // No parameters, no values for parameters
            try
            {
                // Open the connection to the Database
                conn.Open();

                // Read in data
                var reader = cmd.ExecuteReader();   // Create a reader object to read DB lines
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        // ItemID ItemName
                        var item = new Item();
                        item.ItemID = Convert.ToInt32(reader["ItemID"]);
                        item.ItemName = Convert.ToString(reader["Item_Name"]);

                        master_equipment_list_builder.Add(item);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return master_equipment_list_builder;
        }

        // Item Accessors
        // Create Item
        public bool CreateNewDBItem(Item item)
        {
            bool result = false;
            int rowsAffected = 0;
            // Connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            // Command Tet
            var cmdText = "sp_create_new_item";
            // Command
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            // Parameters
            cmd.Parameters.Add("ItemID", SqlDbType.Int);
            cmd.Parameters.Add("Item_Name", SqlDbType.NVarChar, 50);
            cmd.Parameters["ItemID"].Value = item.ItemID;
            cmd.Parameters["Item_Name"].Value = item.ItemName;
            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                result = false;
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            if (rowsAffected > 0)
            {
                result = true;
            }
            return result;
        }
        // Read Item
        public ItemVM RetrieveDBItemVMByItemID(int itemID)
        {
            {
                // Instantiate new ItemVM
                ItemVM item = new ItemVM();

                // Connection
                var connectionFactory = new DBConnection();
                var conn = connectionFactory.GetConnection();
                // Command Tet
                var cmdText = "sp_select_item_from_item_by_itemID";
                // Command
                var cmd = new SqlCommand(cmdText, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                // Parameters
                cmd.Parameters.Add("ItemID", SqlDbType.Int);
                cmd.Parameters["ItemID"].Value = itemID;
                try
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            // Populate the base Item properties
                            if (item.ItemID == null || item.ItemID == 0 || item.ItemName == null || item.ItemName.Equals(""))
                            {
                                // When stored procedure returns AS these need to return in the AS form
                                // int reader_ItemID = Convert.ToInt32(reader["Item ID"]);
                                int reader_ItemID = Convert.ToInt32(reader["ItemID"]);
                                string reader_ItemName = Convert.ToString(reader["Item_Name"]);
                                item.ItemID = reader_ItemID;
                                item.ItemName = reader_ItemName;
                            }
                            // Populate the detailed (Dictionary) item stats
                            string reader_StatName = Convert.ToString(reader["StatID"]);
                            int reader_StatValue = Convert.ToInt32(reader["Stat_Value"]);
                            item.ItemStats.Add(reader_StatName, reader_StatValue);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
                return item;
            }
        }
        // Update Item
        public bool UpdateDBItemByItemVM(ItemVM itemVM, int originalItemID)
        {
            bool result = false;
            // Connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            // Command Tet
            var cmdText = "sp_update_item"; // Create this SP
            // Command
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            // Parameters
            cmd.Parameters.Add("OldItemID", SqlDbType.Int);
            cmd.Parameters.Add("NewItemID", SqlDbType.Int);
            cmd.Parameters.Add("Item_Name", SqlDbType.NVarChar, 50);
            cmd.Parameters["OldItemID"].Value = originalItemID;
            cmd.Parameters["NewItemID"].Value = itemVM.ItemID;
            cmd.Parameters["Item_Name"].Value = itemVM.ItemName;
            try
            {
                conn.Open();
                int rows_returned = cmd.ExecuteNonQuery();
                if (rows_returned > 0)
                {
                    result = true;
                }
            }
            catch (Exception)
            {
                result = false;
                throw;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
        // Delete Item
        public bool DeleteDBItemByItemID(int itemID)
        {
            // Prepare success/fail return
            bool deleteSuccessful = false;
            // Connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            // Command Tet
            var cmdText = "sp_delete_item_by_itemID"; // Create this SP
            // Command
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            // Parameters
            cmd.Parameters.Add("ItemID", SqlDbType.Int);
            cmd.Parameters["ItemID"].Value = itemID;
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                deleteSuccessful = true;
            }
            catch (Exception)
            {
                deleteSuccessful = false;
                throw;
            }
            finally
            {
                conn.Close();
            }
            return deleteSuccessful;
        }

        // Stat Interfaces
        // Create Stat
        public bool AddStatToItemByItemIDNameAndValue(int itemID, string stat_name, int stat_value)
        {
            bool result = false;
            // Connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            // Command Tet
            var cmdText = "sp_add_stat_to_item_by_item_id"; // Create this SP
            // Command
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            // Parameters
            cmd.Parameters.Add("ItemID", SqlDbType.Int);
            cmd.Parameters.Add("StatID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("Stat_Value", SqlDbType.Int);
            cmd.Parameters["ItemID"].Value = itemID;
            cmd.Parameters["StatID"].Value = stat_name;
            cmd.Parameters["Stat_Value"].Value = stat_value;
            try
            {
                conn.Open();
                int rows_returned = cmd.ExecuteNonQuery();
                result = Convert.ToBoolean(rows_returned);
            }
            catch (Exception)
            {
                result = false;
                throw;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }
        // Delete (Single)
        public bool DeteteStatFromItemByIDAndName(int itemID, string stat_name)
        {
            bool result = false;
            // Connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            // Command Tet
            var cmdText = "sp_delete_stat_from_item_by_item_id"; // Create this SP
            // Command
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            // Parameters
            cmd.Parameters.Add("ItemID", SqlDbType.Int);
            cmd.Parameters.Add("StatID", SqlDbType.NVarChar, 50);
            cmd.Parameters["ItemID"].Value = itemID;
            cmd.Parameters["StatID"].Value = stat_name;
            try
            {
                conn.Open();
                int rows_returned = cmd.ExecuteNonQuery();
                result = Convert.ToBoolean(rows_returned);
            }
            catch (Exception)
            {
                result = false;
                throw;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }
        // Delete (All)
        public int DeleteAllStatsFromItemByItemID(int itemID)
        {
            int rows_deleted = 0;
            // Connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            // Command Tet
            var cmdText = "sp_delete_all_stats_on_item_by_itemID"; // Create this SP
            // Command
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            // Parameters
            cmd.Parameters.Add("ItemID", SqlDbType.Int);
            cmd.Parameters["ItemID"].Value = itemID;
            try
            {
                conn.Open();
                int rows_returned = cmd.ExecuteNonQuery();
                rows_deleted = rows_returned;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return rows_deleted;
        }
    }

}
