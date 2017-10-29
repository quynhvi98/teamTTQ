using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace bookstore.Models
{
    public class CustomerAddressModel : ConnectDatabase
    {
        public void creatCustomerAddress(String _name, String _adddress_full, String _phone, String _city, String _district, int _id_customer)
        {
            String sql = "INSERT dbo.customer_address (_name, [_adddress_full] , [_phone] ,[_city] ,[_district] , [_id_customer]) VALUES  ( @_name,@_adddress_full, @_phone,@_city,@_district ,@_id_customer )";
            SqlCommand cmd = new SqlCommand(sql, GetConnection());
            if (cmd.Connection.State == ConnectionState.Closed)
            {
                cmd.Connection.Open();
            }
            cmd.Parameters.AddWithValue("@_name", _name);
            cmd.Parameters.AddWithValue("@_adddress_full", _adddress_full);
            cmd.Parameters.AddWithValue("@_phone", _phone);
            cmd.Parameters.AddWithValue("@_city", _city);
            cmd.Parameters.AddWithValue("@_district", _district);
            cmd.Parameters.AddWithValue("@_id_customer", _id_customer);
            int reuslt = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        public int GetIDCustomerAddressrUniqueByIdCustomer(int id)
        {
            try
            {
                string sql = "SELECT customer_address._id FROM customer_address WHERE _id_customer=@id";
                SqlCommand cmd = new SqlCommand(sql, GetConnection());
                cmd.Connection.Open();
                cmd.Parameters.AddWithValue("@id", id);
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
        public void creatCustomerAddressHaveEmail(String email, String name, String _adddress_full, String _phone, String _city, String _district, int _id_customer)
        {
            String sql = "INSERT dbo.customer_address ( _email,_name,[_adddress_full] , [_phone] ,[_city] ,[_district] , [_id_customer]) VALUES  (@_email,@_name, @_adddress_full, @_phone,@_city,@_district ,@_id_customer )";
            SqlCommand cmd = new SqlCommand(sql, GetConnection());
            if (cmd.Connection.State == ConnectionState.Closed)
            {
                cmd.Connection.Open();
            }
            cmd.Parameters.AddWithValue("@_email", email);
            cmd.Parameters.AddWithValue("@_name", name);
            cmd.Parameters.AddWithValue("@_adddress_full", _adddress_full);
            cmd.Parameters.AddWithValue("@_phone", _phone);
            cmd.Parameters.AddWithValue("@_city", _city);
            cmd.Parameters.AddWithValue("@_district", _district);
            cmd.Parameters.AddWithValue("@_id_customer", _id_customer);
            int reuslt = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        public int GetIDCustomerAddressrTop1UniqueByIdCustomer(int id)
        {
            try
            {
                string sql = "SELECT TOP 1 [_id] FROM dbo.customer_address WHERE [_id_customer]=@id ORDER BY [_id] DESC";
                SqlCommand cmd = new SqlCommand(sql, GetConnection());
                cmd.Connection.Open();
                cmd.Parameters.AddWithValue("@id", id);
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
        public List<CustomerAddress> GetListAddressCustomerByCustomerId(int id)
        {
            List<CustomerAddress> list = new List<CustomerAddress>();
            string sql = "SELECT top 5 * FROM dbo.customer_address WHERE [_id_customer]=@id ";
            SqlCommand cmd = new SqlCommand(sql, GetConnection());
            cmd.Connection.Open();
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                CustomerAddress customerAddress = new CustomerAddress();

                customerAddress.id = dr.GetInt32(0);
                try
                {
                    customerAddress._adddress_full = dr.GetString(1);
                }
                catch (Exception)
                {

                    customerAddress._adddress_full = null;
                }
                try
                {
                    customerAddress._email = dr.GetString(2);
                }
                catch (Exception)
                {

                    customerAddress._email = null;
                }

                try
                {
                    customerAddress._phone = dr.GetString(3);
                }
                catch (Exception)
                {

                    customerAddress._phone = null;
                }

                try
                {
                    customerAddress._company = dr.GetString(4);

                }
                catch (Exception)
                {

                    customerAddress._company =null;
                }
                try
                {
                    customerAddress._zipe_code = dr.GetString(5);
                }
                catch (Exception)
                {

                    customerAddress._zipe_code = null;
                }
                try
                {
                    customerAddress._nation = dr.GetString(6);
                }
                catch (Exception)
                {

                    customerAddress._nation =null;
                }
                try
                {
                    customerAddress._city = dr.GetString(6);
                }
                catch (Exception)
                {

                    customerAddress._city = null;
                }
                try
                {
                    customerAddress._district = dr.GetString(8);
                }
                catch (Exception)
                {

                    customerAddress._district = null;
                }
                    customerAddress._id_customer = dr.GetInt32(9);
             
                try
                {
                    customerAddress._name = dr.GetString(10);
                }
                catch (Exception)
                {

                    customerAddress._name = null;
                }
                list.Add(customerAddress);
            }
            cmd.Connection.Close();

            return list;
        }
        public bool MailToCustomer(string email)
        {
            try
            {
                StringBuilder Body = new StringBuilder();
                Body.Append("<p>Chúc mừng bạn đã đặt hàng thành công, nhân viên của chúng tôi sẽ sớm gọi điện lại cho bạn để xác nhận.</p>");     
                MailMessage mail = new MailMessage();
                mail.To.Add(email);//đổi mail của mình để test
                mail.From = new MailAddress("projectsem2aptech@gmail.com");
                mail.Subject = "Edubook.com.vn";
                mail.Body = Body.ToString();
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = new System.Net.NetworkCredential("projectsem2aptech@gmail.com", "Chicanem1");
                smtp.EnableSsl = true;
                smtp.Send(mail);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}