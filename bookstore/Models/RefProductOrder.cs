using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bookstore.Models
{
    public class RefProductOrder
    {
       public int _quantity { get; set; }
        public int _id_order { get; set; }
        public String _id_product { get; set; }
        public Double _price { get; set; }
    }
}