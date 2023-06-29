using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using DataAccessLayerInterfaces;

namespace DataAccessLayer
{
    internal class DBConnection : IDBConnection // C# way of saying this class implements the Interface IDBConnection
    // internal class DBConnection
    {
        public SqlConnection GetConnection()
        {
            SqlConnection conn = null;
            // This connection string is local to this PC
            string connectionString = @"Data Source=localhost;Initial Catalog=ffxi_equipment_db;Integrated Security=True";
            conn = new SqlConnection(connectionString);

            return conn;
        }
    }
}
