using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bookstore.Models
{
    public class CustomerAddress
    {
        public int id { get; set; }
        public String _adddress_full { get; set; }
        public String _email { get; set; }
        public String _phone { get; set; }
        public String _company { get; set; }
        public String _zipe_code { get; set; }
        public String _nation { get; set; }
        public String _city { get; set; }
        public String _district { get; set; }
        public int _id_customer { get; set; }
        public String _name { get; set; }


    }
}