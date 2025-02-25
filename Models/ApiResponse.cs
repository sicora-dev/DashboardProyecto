using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardTienda.Models
{
    public class ApiResponse
    {
        public string message { get; set; }
        public int status { get; set; }
        public string token { get; set; }
        public List<Order> orders { get; set; }
    }
}
