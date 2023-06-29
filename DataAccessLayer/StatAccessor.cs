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
    public class StatAccessor : IStatAccessor
    {
        public int AddStat(string statName)
        {
            int rowsAffected = 0;
            // Connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            // Command Tet
            var cmdText = "sp_add_stat";
            // Command
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            // Parameters
            cmd.Parameters.Add("StatID", SqlDbType.NVarChar, 50);
            cmd.Parameters["StatID"].Value = statName;
            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return rowsAffected;
        }

        public int AddStatAlias(string statName, string alias)
        {
            int rowsAffected = 0;
            if (alias.Contains('|'))
            {
                throw new ArgumentException("Illegal character, stat alias Rejected");
            }
            // Connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            // Command Tet
            var cmdText = "sp_add_stat_alias";
            // Command
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            // Parameters
            cmd.Parameters.Add("StatID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("StatAlias", SqlDbType.NVarChar, 100);
            cmd.Parameters["StatID"].Value = statName;
            cmd.Parameters["StatAlias"].Value = alias;
            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return rowsAffected;
        }

        public int DeleteStatAlias(string statName, string alias)
        {
            int rowsAffected = 0;
            // Emergency validation
            if (alias.Equals(statName))
            {
                throw new ArgumentException("Illegal name change, you may not remove the key alias");
            }
            // Connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            // Command Tet
            var cmdText = "sp_delete_stat_alias";
            // Command
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            // Parameters
            cmd.Parameters.Add("StatID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("StatAlias", SqlDbType.NVarChar, 100);
            cmd.Parameters["StatID"].Value = statName;
            cmd.Parameters["StatAlias"].Value = alias;
            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return rowsAffected;
        }

        public int RenameStat(string oldStatName, string newStatName)
        {
            int rowsAffected = 0;
            // Connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            // Command Tet
            var cmdText = "sp_update_stat";
            // Command
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            // Parameters
            cmd.Parameters.Add("NewStatID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("OldStatID", SqlDbType.NVarChar, 50);
            cmd.Parameters["NewStatID"].Value = newStatName;
            cmd.Parameters["OldStatID"].Value = oldStatName;
            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return rowsAffected;
        }

        public int RenameStatAlias(string statName, string oldAlias, string newAlias)
        {
            int rowsAffected = 0;
            // Connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            // Command Tet
            var cmdText = "sp_update_stat_alias";
            // Command
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            // Parameters
            cmd.Parameters.Add("StatID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("NewStatAlias", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("OldStatAlias", SqlDbType.NVarChar, 100);
            cmd.Parameters["StatID"].Value = statName;
            cmd.Parameters["NewStatAlias"].Value = newAlias;
            cmd.Parameters["OldStatAlias"].Value = oldAlias;
            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return rowsAffected;
        }

        public List<StatVM> SelectAllStatVMs()
        {
            List<StatVM> statVMListBuilder = new List<StatVM>();
            // Connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            // Command Tet
            var cmdText = "sp_select_all_stats_aliases";
            // Command
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            // No parameters, no values for parameters
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string statName = Convert.ToString(reader["StatID"]);
                    string concatenatedAliases = Convert.ToString(reader["Alias"]);
                    List<string> aliasList = new List<string>();
                    if (concatenatedAliases != null && concatenatedAliases.Length > 0)
                    {
                        string[] aliases = concatenatedAliases.Split('|');
                        foreach (string alias in aliases)
                        {
                            if (alias.Length > 0)
                            {
                                aliasList.Add(alias);
                            }
                        }
                    }
                    // Failsafe if aliases are missing, or if key alias is missing
                    if (!aliasList.Contains(statName))
                    {
                        aliasList.Add(statName);
                    }
                    statVMListBuilder.Add(new StatVM { Name = statName, AliasList = aliasList });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Close the connection to the Database
                conn.Close();
            }
            return statVMListBuilder;
        }

        public StatVM SelectStatVMByAlias(string nameOfAlias)
        {
            StatVM stat = new StatVM();
            stat.AliasList = new List<string>();

            string nameCheck = null;
            HashSet<string> aliasesToAdd = new HashSet<string>();   // This will prevent any duplicate stat entries, just in case
            // Connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            // Command Tet
            var cmdText = "sp_select_stat_by_stat_alias";
            // Command
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            // Parameters
            cmd.Parameters.Add("StatAlias", SqlDbType.NVarChar, 100);
            cmd.Parameters["StatAlias"].Value = nameOfAlias;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string statName = Convert.ToString(reader["StatID"]);
                    string statAlias = Convert.ToString(reader["StatAlias"]);
                    // Make sure this is only returning one StatID
                    if (nameCheck == null && statName != null && !statName.Equals(""))
                    {
                        nameCheck = statName;
                    }
                    else if (nameCheck != statName)
                    {
                        throw new ApplicationException("Search result returned two or more Stat IDs, bugfix this search: " + nameOfAlias);
                    }
                    if(statAlias != null && !statAlias.Equals(""))
                    {
                        aliasesToAdd.Add(statAlias);
                    }
                }
                // Set item's values
                stat.Name = nameCheck;
                foreach(string hashSetStat in aliasesToAdd)
                {
                    stat.AliasList.Add(hashSetStat);
                }
                // Failsafe if aliases are missing, or if key alias is missing
                if (!stat.AliasList.Contains(nameCheck))
                {
                    stat.AliasList.Add(nameCheck);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Close the connection to the Database
                conn.Close();
            }
            return stat;
        }
    }
}
