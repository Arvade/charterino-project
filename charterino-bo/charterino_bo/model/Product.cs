using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace charterino_bo.model
{
    public class Product {
        public int id { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public double price { get; set; }
        public int sold { get; set; }

        public Product(int id, string name, string category, double price, int sold)
        {
            this.id = id;
            this.name = name;
            this.category = category;
            this.price = price;
            this.sold = sold;
        }
    }
}