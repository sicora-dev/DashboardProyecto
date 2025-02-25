using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardTienda.Models
{
    public class Order
    {
        public required string _id { get; set; }
        public DateTime date { get; set; }
        public required List<OrderItem> items { get; set; }
        public int payment_method { get; set; }
        public decimal total_amount { get; set; }
        public required string user_id { get; set; }
    }
}
