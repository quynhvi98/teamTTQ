using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace bookstore.Models
{
    public class CustomerModel : ConnectDatabase
    {
        public bool LoginWithAccAndPass(String userAccount, String password)
        {
            String sql = "SELECT* FROM customer WHERE _user = @userAccount AND _password = @password";
            SqlCommand cmd = new SqlCommand(sql, GetConnection());
            if (cmd.Connection.State == ConnectionState.Closed)
            {
                cmd.Connection.Open();
            }
            cmd.Parameters.AddWithValue("@userAccount", userAccount);
            cmd.Parameters.AddWithValue("@password", password);
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.HasRows != true)
            {
                String sql1 = "SELECT * FROM customer WHERE  _email=@userAccount AND _password=@password";
                SqlCommand md1 = new SqlCommand(sql1, GetConnection());
                if (md1.Connection.State == ConnectionState.Closed)
                {
                    md1.Connection.Open();
                }
                md1.Parameters.AddWithValue("@userAccount", userAccount);
                md1.Parameters.AddWithValue("@password", password);
                SqlDataReader rd1 = md1.ExecuteReader();
                bool check = rd1.HasRows;
                md1.Connection.Close();
                return check;
            }
            cmd.Connection.Close();

            return true;
        }
        public void creatCustomer(String email,String user, String name)
        {
            String sql = "INSERT dbo.customer( [_email],_user ,[_name],_password  )VALUES  ( @email,@user,@name,@password )";
            SqlCommand cmd = new SqlCommand(sql, GetConnection());
            if (cmd.Connection.State == ConnectionState.Closed)
            {
                cmd.Connection.Open();
            }
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@user", user);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@password","123456");
            int reuslt = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        public int GetIDCustomerByEmail(String email)
        {
            try
            {
                string sql = "SELECT [_id] FROM dbo.customer WHERE [_email]=@email";
                SqlCommand cmd = new SqlCommand(sql, GetConnection());
                cmd.Connection.Open();
                cmd.Parameters.AddWithValue("@email", email);
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
        public String GetuserByEmail(String email)
        {
            try
            {
                string sql = "SELECT [_user] FROM dbo.customer WHERE [_email]=@email";
                SqlCommand cmd = new SqlCommand(sql, GetConnection());
                cmd.Connection.Open();
                cmd.Parameters.AddWithValue("@email", email);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    return dr.GetString(0);
                }
            }
            catch (Exception e)
            {
                return null;
            }
            return null;
        }
        public int GetIDCustomerByUser(String User)
        {
            try
            {
                string sql = "SELECT [_id] FROM dbo.customer WHERE [_user]=@User";
                SqlCommand cmd = new SqlCommand(sql, GetConnection());
                cmd.Connection.Open();
                cmd.Parameters.AddWithValue("@User", User);
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
        public void Signin(String email, String user, String password, String name)
        {
            String sql = "INSERT dbo.customer( [_email],_user ,[_name],_password  )VALUES  ( @email,@user,@name,@password )";
            SqlCommand cmd = new SqlCommand(sql, GetConnection());
            if (cmd.Connection.State == ConnectionState.Closed)
            {
                cmd.Connection.Open();
            }
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@user", user);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@password", password);
            int reuslt = cmd.ExecuteNonQuery();
        }

    }
}