using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bookstore.Models
{
    public class Book
    {
        public string _id { get; set; }
        public string _name { get; set; }
        public string _img { get; set; }
        public double _price { get; set; }
        public double _price_pages { get; set; }
        public int _pages { get; set; }
        public double _weight { get; set; }
        public string _content { get; set; }
        public string _status { get; set; }
        public string _year_of_creation { get; set; }
        public string _producer { get; set; }
        public string _type { get; set; }
        public string _author { get; set; }
        public string _category { get; set; }
        
    }
}