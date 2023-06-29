using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;

namespace DataAccessLayerInterfaces
{
    public interface IDBConnection
    {
        // This interface defines how this program can perform SQL database connection
        SqlConnection GetConnection();
    }
}
