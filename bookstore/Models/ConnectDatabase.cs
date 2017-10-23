using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace bookstore.Models
{
    public class ConnectDatabase
    {
        public SqlConnection GetConnection()
        {
            SqlConnection conn = new SqlConnection("server=localhost;database=BookASMWAD;uid=sa;pwd=123456");
            return conn;
        }
    }
}