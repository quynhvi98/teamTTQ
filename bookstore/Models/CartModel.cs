using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace bookstore.Models
{
    public class CartModel : ConnectDatabase
    {
        public List<Cart> getCartByIdUser(String id)
        {
            string sql = "SELECT dbo.cart.[_id],id_user,id_product,quantity,[_IMG],[_price],[_price_pages],[_name],[_repository] FROM cart JOIN dbo.product ON product.[_id] = cart.id_product  WHERE id_user='"+id+ "'";
            SqlDataAdapter da = new SqlDataAdapter(sql, GetConnection());
            DataTable dt = new DataTable();
            da.Fill(dt);
            return Table_to_List(dt);
        }

        private List<Cart> Table_to_List(DataTable dt)
        {
            List<Cart> list = new List<Cart>();
            foreach (DataRow item in dt.Rows)
            {
                Cart cart = new Cart( );
                cart._id = Int32.Parse(item[0].ToString());

                cart.id_product =item[2].ToString();
                cart.quantity = Int32.Parse(item[3].ToString());
                cart.img = item[4].ToString();
                cart.price = Convert.ToDouble(item[5].ToString());
                cart.price_pages = Convert.ToDouble(item[6].ToString());
                cart.name_product = item[7].ToString();
                cart.repository = Convert.ToInt32(item[8].ToString());
                list.Add(cart);
            }
            return list;
        }
        public Decimal gettongtien(String id)
        {
            string sql = "SELECT SUM(dbo.product.[_price_pages]*quantity) AS 'dsd'  FROM cart JOIN dbo.product ON product.[_id] = cart.id_product WHERE id_user ='"+id+"'";
            SqlCommand md1 = new SqlCommand(sql, GetConnection());

            if (md1.Connection.State == ConnectionState.Closed)
            {
                md1.Connection.Open();
            }
            SqlDataReader rd1 = md1.ExecuteReader();
            if (rd1.HasRows)
            {
                while (rd1.Read())
                {
                    try
                    {
                     return rd1.GetDecimal(0);
                    }
                    catch (Exception e)
                    {

                    }
               
                }
                   
            }
            return 0;
        }

        public void updatecart(String quanti,String user,String product)
        {
            String sql = "UPDATE dbo.cart SET quantity=@quanti WHERE id_user=@user AND id_product=@product";
            SqlCommand cmd = new SqlCommand(sql, GetConnection());
            if (cmd.Connection.State == ConnectionState.Closed)
            {
                cmd.Connection.Open();
            }
            cmd.Parameters.AddWithValue("@quanti", quanti);
            cmd.Parameters.AddWithValue("@user", user);
            cmd.Parameters.AddWithValue("@product", product);
            int reuslt = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        public void creatAndUpdate(String user, String product)
        {
            string sql = "SELECT * FROM dbo.cart WHERE id_user=@user AND id_product=@product";
            SqlCommand md1 = new SqlCommand(sql, GetConnection());
            if (md1.Connection.State == ConnectionState.Closed)
            {
                md1.Connection.Open();
            }
            md1.Parameters.AddWithValue("@user", user);
            md1.Parameters.AddWithValue("@product", product);
            SqlDataReader rd1 = md1.ExecuteReader();
            
            if (rd1.Read())
            {
                md1.Connection.Close();

                sql = "UPDATE dbo.cart SET quantity=quantity+ CASE WHEN quantity>=30 THEN 0 ELSE 1 END  WHERE id_user=@user AND id_product=@product";
                SqlCommand md12 = new SqlCommand(sql, GetConnection());
                if (md12.Connection.State == ConnectionState.Closed)
                {
                    md12.Connection.Open();
                }
                md12.Parameters.AddWithValue("@user", user);
                md12.Parameters.AddWithValue("@product", product);
                SqlDataReader rd11 = md12.ExecuteReader();
            }
            else
            {
                md1.Connection.Close();
                sql = "INSERT dbo.cart( id_user, id_product, quantity )VALUES  (@user,@product, 1)";
                SqlCommand md12 = new SqlCommand(sql, GetConnection());
                if (md12.Connection.State == ConnectionState.Closed)
                {
                    md12.Connection.Open();
                }
                md12.Parameters.AddWithValue("@user", user);
                md12.Parameters.AddWithValue("@product", product);
                SqlDataReader rd11 = md12.ExecuteReader();
            }
        }
        public List<RefProductOrder> GetlistProductFromIdUser(String id)
        {
            string sql = "SELECT id_product,quantity,[_price_pages] FROM  dbo.cart JOIN dbo.product ON product.[_id] = cart.id_product  WHERE id_user='"+id+"'";
            SqlDataAdapter da = new SqlDataAdapter(sql, GetConnection());
            DataTable dt = new DataTable();
            da.Fill(dt);
            return TablelistProductFromIdUser(dt);
        }
        private List<RefProductOrder> TablelistProductFromIdUser(DataTable dt)
        {
            List<RefProductOrder> list = new List<RefProductOrder>();
            foreach (DataRow item in dt.Rows)
            {
                RefProductOrder cart = new RefProductOrder();
                cart._id_product = item[0].ToString();
                cart._quantity = int.Parse(item[1].ToString());
                cart._price = Double.Parse(item[2].ToString());
                list.Add(cart);
            }
            return list;
        }
        public void Deletecart( String user)
        {
            String sql = "DELETE FROM dbo.cart  WHERE id_user=@user";
            SqlCommand cmd = new SqlCommand(sql, GetConnection());
            if (cmd.Connection.State == ConnectionState.Closed)
            {
                cmd.Connection.Open();
            }
            cmd.Parameters.AddWithValue("@user", user);
            int reuslt = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        public void DeletecartFromUserIdAndProductId(String user,String idProduct)
        {
            String sql = "DELETE FROM dbo.cart WHERE id_user=@user AND id_product=@id_product";
            SqlCommand cmd = new SqlCommand(sql, GetConnection());
            if (cmd.Connection.State == ConnectionState.Closed)
            {
                cmd.Connection.Open();
            }
            cmd.Parameters.AddWithValue("@user", user);
            cmd.Parameters.AddWithValue("@id_product", idProduct);
            int reuslt = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        public List<Cart> GetlistCartFromIdUser(String id)
        {
            string sql = "SELECT * FROM cart WHERE id_user='"+id+ "'";
            SqlDataAdapter da = new SqlDataAdapter(sql, GetConnection());
            DataTable dt = new DataTable();
            da.Fill(dt);
            return TablelistCartFromIdUserr(dt);
        }
        private List<Cart> TablelistCartFromIdUserr(DataTable dt)
        {
            List<Cart> list = new List<Cart>();
            foreach (DataRow item in dt.Rows)
            {
                Cart cart = new Cart();
                cart.id_user = item[1].ToString();
                cart.id_product = item[2].ToString();
                cart.quantity = Int32.Parse(item[3].ToString());
                list.Add(cart);
            }
            return list;
        }

        public void UpdateIdserFromIdCustomer(String user, String Customer)
        {
            List<Cart> IdUser = GetlistCartFromIdUser(user);
            List<Cart> IdCustomer = GetlistCartFromIdUser(Customer);


            for (int i = 0; i < IdUser.Count; i++)
            {
                int a = IdCustomer.IndexOf(IdUser[i]);
                if (a >= 0)
                {
                    updateCustomerHad(user, Customer, IdUser[i].id_product, IdUser[i].quantity);
                }
                
            }

            String sql = "UPDATE  cart SET  id_user=@Customer WHERE  id_user=@user";
            SqlCommand cmd = new SqlCommand(sql, GetConnection());
            if (cmd.Connection.State == ConnectionState.Closed)
            {
                cmd.Connection.Open();
            }
            cmd.Parameters.AddWithValue("@Customer", Customer);
            cmd.Parameters.AddWithValue("@user", user);
            int reuslt = cmd.ExecuteNonQuery();
            cmd.Connection.Close();

        }
        public void updateCustomerHad(String user, String Customer,String idProdcut,int quantity)
        {
            String sql = "UPDATE  cart SET quantity=quantity+@quantity WHERE  id_user=@Customer and id_product=@idProdcut";
            SqlCommand cmd = new SqlCommand(sql, GetConnection());
            if (cmd.Connection.State == ConnectionState.Closed)
            {
                cmd.Connection.Open();
            }
            cmd.Parameters.AddWithValue("@Customer", Customer);
            cmd.Parameters.AddWithValue("@idProdcut", idProdcut);
            cmd.Parameters.AddWithValue("@quantity", quantity);
            int reuslt = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            //
            String sql1 = "DELETE FROM cart WHERE  id_user=@user and id_product=@product";
            SqlCommand cmd1 = new SqlCommand(sql1, GetConnection());
            if (cmd1.Connection.State == ConnectionState.Closed)
            {
                cmd1.Connection.Open();
            }
            cmd1.Parameters.AddWithValue("@user", user);
            cmd1.Parameters.AddWithValue("@product", idProdcut);
            int reuslt1 = cmd1.ExecuteNonQuery();
            cmd1.Connection.Close();
        }
    }
}