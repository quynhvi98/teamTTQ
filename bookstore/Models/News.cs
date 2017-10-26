using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bookstore.Models
{
    public class News
    {
        public int _id { get; set; }
        public string _name { get; set; }
        public string _img { get; set; }
        public string _content { get; set; }
        public string _author { get; set; }
        public DateTime _date { get; set; }
        public string _short_title { get; set; }
    }
}