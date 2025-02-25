using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardTienda.Models
{
    public class OrderItem
    {
        public decimal price { get; set; }
        public required string product_id { get; set; }
        public int quantity { get; set; }
        public required string size { get; set; }
    }
}
