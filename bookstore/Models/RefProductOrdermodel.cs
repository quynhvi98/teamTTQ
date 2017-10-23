using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace bookstore.Models
{
    public class RefProductOrdermodel :ConnectDatabase
    {
        public void creatRefProductOrder(RefProductOrder refProductOrder,int idOrder)
        {
            String sql = "INSERT dbo.ref_product_order( [_quantity] ,[_id_order] , [_id_product] ,[_price] )VALUES " +
                " ( @_quantity , @_id_order , @_id_product , @_price )";
            SqlCommand cmd = new SqlCommand(sql, GetConnection());
            if (cmd.Connection.State == ConnectionState.Closed)
            {
                cmd.Connection.Open();
            }
            cmd.Parameters.AddWithValue("@_quantity", refProductOrder._quantity);
            cmd.Parameters.AddWithValue("@_id_order", idOrder);
            cmd.Parameters.AddWithValue("@_id_product", refProductOrder._id_product);
            cmd.Parameters.AddWithValue("@_price", refProductOrder._price);
            int reuslt = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
    }
}