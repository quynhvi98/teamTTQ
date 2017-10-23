using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bookstore.Models
{
    public class Cart
    {
        public int _id { get; set; }
        public String id_user { get; set; }
        public String id_product { get; set; }
        public string img { get; set; }
        public int quantity { get; set; }
        public double price { get; set; }
        public double price_pages { get; set; }
        public string name_product { get; set; }
        public int repository { get; set; }
        public override bool Equals(object obj)
        {
            if(obj is Cart)
            {
                var item = obj as Cart;
                if (item.id_product.Equals(this.id_product))
                {
                    return true;
                }
            }
            return false;
        }

    }
}