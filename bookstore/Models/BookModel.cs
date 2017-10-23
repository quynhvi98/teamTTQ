using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using bookstore.Models;

namespace bookstore.Models
{
    public class BookModel : ConnectDatabase
    {
        public List<Book> GetBooks(string query1, string query2)
        {
            string sql = "SELECT " + query1 + " product.[_id],product.[_name],product.[_IMG],[_price],[_pages],[_weight],[_content],[_status]," +
                "[_year_of_creation],dbo.producer.[_name],dbo.product_type.[_name],[_name_author],dbo.category.[_name] " +
                "FROM dbo.product JOIN dbo.product_type ON product_type.[_id] = product.[_type] JOIN dbo.category_product" +
                " ON category_product.[_id_product] = product.[_id] JOIN dbo.producer ON producer.[_id] = product.[_id_producer]" +
                " JOIN dbo.author ON author.[_id] = product.[_author] JOIN dbo.category ON category.[_id] = " +
                "category_product.[_id_category] " + query2;
            SqlDataAdapter da = new SqlDataAdapter(sql, GetConnection());
            DataTable dt = new DataTable();
            da.Fill(dt);
            return Table_to_List(dt);
        }

        public List<Book> Table_to_List(DataTable dt)
        {
            List<Book> list = new List<Book>();
            foreach (DataRow item in dt.Rows)
            {
                Book book = new Book()
                {
                    _id = item[0].ToString(),
                    _name = item[1].ToString(),
                    _img = item[2].ToString(),
                    _price = Convert.ToDouble(item[3].ToString()),
                    _pages = Convert.ToInt32(item[4].ToString()),
                    _weight = Convert.ToDouble(item[5].ToString())*1000,
                    _content = item[6].ToString(),
                    _status = item[7].ToString(),
                    _year_of_creation = item[8].ToString(),
                    _producer = item[9].ToString(),
                    _type = item[10].ToString(),
                    _author = item[11].ToString(),
                    _category = item[12].ToString()
                };
                list.Add(book);
            }
            return list;
        }

        public double GetRating(string id_book)
        {
            double rating = 0;
            try
            {
               
                string sql = "SELECT AVG([_rating]) AS _rating FROM dbo.review WHERE [_id_Product] = '" + id_book + "'";
                SqlCommand cmd = new SqlCommand(sql, GetConnection());
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                    rating = dr.GetInt32(0);
                cmd.Connection.Close();
            }catch(Exception e)
            {
                rating = 5;
            }
          
            return rating;
        }

        public Book GetBookByID(string id)
        {
            Book book = null;
            string sql = "SELECT  product.[_id],product.[_name],product.[_IMG],[_price],[_pages],[_weight],[_content],[_status]," +
                "[_year_of_creation], dbo.producer.[_name],dbo.product_type.[_name],[_name_author] FROM dbo.product JOIN" +
                " dbo.product_type ON product_type.[_id] = product.[_type] JOIN dbo.producer ON producer.[_id] =" +
                " product.[_id_producer] JOIN dbo.author ON author.[_id] = product.[_author] WHERE product.[_id] = '" + id + "'";
            SqlCommand cmd = new SqlCommand(sql, GetConnection());
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                book = new Book()
                {
                    _id = dr.GetString(0),
                    _name = dr.GetString(1),
                    _img = dr.GetString(2),
                    _price = dr.GetDouble(3),
                    _pages = dr.GetInt32(4),
                    _weight = dr.GetDouble(5)*1000,
                    _content = dr.GetString(6),
                    _status = dr.GetString(7),
                    _year_of_creation = dr.GetInt32(8).ToString(),
                    _producer = dr.GetString(9),
                    _type = dr.GetString(10),
                    _author = dr.GetString(11),
                    _category = "",
                };
            }

            cmd.Connection.Close();

            return book;
        }

        public List<Book> searchByName(string query)
        {
            string sql = "SELECT  product.[_id],dbo.product.[_name],[_IMG],AVG([_rating]) AS _rating FROM dbo.product LEFT JOIN dbo.review ON review.[_id_Product] = product.[_id] WHERE [_name] LIKE N'%" + query + "%' GROUP BY  dbo.product.[_id],dbo.product.[_name],dbo.product.[_IMG]";
            SqlDataAdapter da = new SqlDataAdapter(sql, GetConnection());
            DataTable dt = new DataTable();
            da.Fill(dt);
            return Table_to_List_Search(dt);
        }

        public List<Book> Table_to_List_Search(DataTable dt)
        {
            List<Book> list = new List<Book>();
            foreach (DataRow item in dt.Rows)
            {
                Book book = new Book()
                {
                    _id = item[0].ToString(),
                    _name = item[1].ToString(),
                    _img = item[2].ToString(),
                };
                list.Add(book);
            }
            return list;
        }

        public List<Book> GetBooksForSlider()
        {
            List<Book> list = new List<Book>();
            string sql = "SELECT TOP 12 [_id],[_name],[_IMG] FROM dbo.product";
            SqlCommand cmd = new SqlCommand(sql, GetConnection());
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Book book = new Book()
                {
                    _id = dr.GetString(0),
                    _name = dr.GetString(1),
                    _img = dr.GetString(2),

                };
                list.Add(book);
            }
            cmd.Connection.Close();

            return list;
        }
    }
}