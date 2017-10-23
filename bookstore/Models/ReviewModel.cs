using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace bookstore.Models
{
    public class ReviewModel : ConnectDatabase
    {

        public List<Review> GetReviews(string id)
        {
            string sql = "SELECT TOP 5 review.[_id],customer.[_name],review.[_date],[_comment],[_rating] FROM dbo.review JOIN dbo.product ON product.[_id] = review.[_id_Product] JOIN dbo.customer ON customer.[_id] = review.[_id_customer] WHERE product.[_id] = '"+id+"' ORDER BY review.[_date] DESC";
            SqlDataAdapter da = new SqlDataAdapter(sql, GetConnection());
            DataTable dt = new DataTable();
            da.Fill(dt);
            return Table_to_List(dt);
        }

        private List<Review> Table_to_List(DataTable dt)
        {
            List<Review> list = new List<Review>();
            foreach (DataRow item in dt.Rows)
            {
                Review review = new Review()
                {
                    _id = Int32.Parse(item[0].ToString()),
                    _name_customer = item[1].ToString(),
                    _date = DateTime.Parse(item[2].ToString()),
                    _comment = item[3].ToString(),
                    _rating = Int32.Parse(item[4].ToString())*20,
                };
                list.Add(review);
            }
            return list;
        }
    }
}