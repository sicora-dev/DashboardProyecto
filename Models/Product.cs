using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardTienda.Models
{
    public class Product
    {
        public required string _id { get; set; }
        public required string name { get; set; }
        public required string description { get; set; }
        public required string img { get; set; }
        public int price { get; set; }
        public int category_id { get; set; }
        public required List<Size> sizes { get; set; }
    }
}
