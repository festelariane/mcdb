using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace Mercedes.Data.Repositories
{
    public class BaseRepository
    {
        public string ConnectionString { get; protected set; }
        public BaseRepository()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
        }
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
