using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace bookstore.Models
{
    public class AuthorModel : ConnectDatabase
    {
        public List<Author> GetAuthors()
        {
            string sql = "SELECT top 4 * FROM dbo.author ";
            SqlDataAdapter da = new SqlDataAdapter(sql, GetConnection());
            DataTable dt = new DataTable();
            da.Fill(dt);
            return Table_to_List(dt);
        }

        public List<Author> Table_to_List(DataTable dt)
        {
            List<Author> list = new List<Author>();
            foreach (DataRow item in dt.Rows)
            {
                Author author = new Author()
                {
                    id_author = item[0].ToString(),
                    name_author = item[1].ToString(),
                    img_author = item[3].ToString(),
                    description = item[2].ToString(),
                  };
                list.Add(author);
            }
            return list;
        }
    }
}