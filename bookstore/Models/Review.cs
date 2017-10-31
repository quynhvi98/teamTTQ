using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bookstore.Models
{
    public class Review
    {
        public int _id { get; set; }
        public string _name_customer { get; set; }
        public DateTime _date { get; set; }
        public string _comment { get; set; }
        public int _rating { get; set; }
        public string _title { get; set; }
    }
}