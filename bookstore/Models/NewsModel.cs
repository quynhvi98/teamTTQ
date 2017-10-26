using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace bookstore.Models
{
    public class NewsModel : ConnectDatabase
    {
        public List<News> GetListNews(int type)
        {
            List<News> list = new List<News>();
            News news = null;

            string sql = null;

            if (type == 1)
                sql = "SELECT TOP 4 id_new,title,img,date_new,content_new,author_new,short_title FROM dbo.book_news ORDER BY id_new DESC";
            if (type == 2)
                sql = "SELECT id_new, title, img, date_new, content_new,author_new,short_title FROM dbo.book_news";
            if (type == 3)
                sql = "SELECT TOP 3 id_new,title,img,date_new,content_new,author_new,short_title FROM dbo.book_news ORDER BY id_new DESC";
            if (type == 4)
                sql = "SELECT TOP 8 id_new,title,img,date_new,content_new,author_new,short_title FROM dbo.book_news ORDER BY id_new DESC";
            SqlCommand cmd = new SqlCommand(sql, GetConnection());
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                news = new News()
                {
                    _id = dr.GetInt32(0),
                    _name = dr.GetString(1),
                    _img = dr.GetString(2),
                    _date = dr.GetDateTime(3),
                    _content = dr.GetString(4),
                    _author = dr.GetString(5),
                    _short_title = dr.GetString(6)
                };
                list.Add(news);
            }

            cmd.Connection.Close();
            return list;
        }

        public News GetNewsByID(string id_news)
        {
            News news = null;

            string sql = "SELECT id_new,title,img,date_new,content_new,author_new FROM dbo.book_news WHERE id_new = '" + id_news + "'";
            SqlCommand cmd = new SqlCommand(sql, GetConnection());
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                news = new News()
                {
                    _id = dr.GetInt32(0),
                    _name = dr.GetString(1),
                    _img = dr.GetString(2),
                    _date = dr.GetDateTime(3),
                    _content = dr.GetString(4),
                    _author = dr.GetString(5)
                };

            }

            cmd.Connection.Close();

            return news;
        }
    }
}
