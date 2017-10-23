using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace bookstore.Models
{
    public class CategoryModel : ConnectDatabase
    {
        public List<Book> GetBookByCategory(string name_type)
        {
            string sql = "SELECT dbo.product.[_id],dbo.product.[_name],[_IMG],[_price],[_price_pages] FROM dbo.product JOIN dbo.product_type ON product_type.[_id] = product.[_type] WHERE product_type.[_name] = N'"+name_type+"'";
            SqlDataAdapter da = new SqlDataAdapter(sql, GetConnection());
            DataTable dt = new DataTable();
            da.Fill(dt);
            return Table_to_List(dt);
        }

        private List<Book> Table_to_List(DataTable dt)
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
                    _price_pages = Convert.ToDouble(item[4].ToString()),
                    
                };
                list.Add(book);      
            }
            return list;
        }
    }
}