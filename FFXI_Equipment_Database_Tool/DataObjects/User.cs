using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        // Four access levels: Administrator, Contributor, Registered_User, Anonymous_User
        // Roles Should be inclusive in descending order, but some future change could alter that
        public List<string> UserRoles { get; set; }
    }
}
