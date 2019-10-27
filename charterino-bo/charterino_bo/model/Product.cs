using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace charterino_bo.model
{
    public class Product {
        private int id { get; set; }
        private string name { get; set; }
        private double price { get; set; }
        private DateTime expiration_date { get; set; } 
        private int sold { get; set; }
    }
}
