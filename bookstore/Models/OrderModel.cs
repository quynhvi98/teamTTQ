using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace bookstore.Models
{
    public class OrderModel : ConnectDatabase
    {
        public void creatOrder(Decimal _total_bill, int _customer, int _id_customer_address)
        {
            String sql = "INSERT dbo.order_product( [_total_bill],_payment_id,_customer,_id_customer_address)VALUES  " +
                "( @_total_bill,1,@_customer,@_id_customer_address)";
            SqlCommand cmd = new SqlCommand(sql, GetConnection());
            if (cmd.Connection.State == ConnectionState.Closed)
            {
                cmd.Connection.Open();
            }
            cmd.Parameters.AddWithValue("@_total_bill", _total_bill);
            cmd.Parameters.AddWithValue("@_customer", _customer);
            cmd.Parameters.AddWithValue("@_id_customer_address", _id_customer_address);
            int reuslt = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        public int GetIDOrderFromTotalBillIdCustomrAndCustomerAddress(Decimal _total_bill, int _customer, int _id_customer_address)
        {
            try
            {
                string sql = "SELECT [_id] FROM  dbo.order_product WHERE [_total_bill]=@_total_bill AND [_customer] =@_customer AND [_id_customer_address]=_id_customer_address";
                SqlCommand cmd = new SqlCommand(sql, GetConnection());
                cmd.Connection.Open();
                cmd.Parameters.AddWithValue("@_total_bill", _total_bill);
                cmd.Parameters.AddWithValue("@_customer", _customer);
                cmd.Parameters.AddWithValue("@_id_customer_address", _id_customer_address);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    return dr.GetInt32(0);
                }
            }
            catch (Exception e)
            {
                return -1;
            }
            return -1;
        }
    }
}