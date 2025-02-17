using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardTienda.Models
{
    public class LogedUser
    {
        public required string id { get; set; }
        public required string mail { get; set; }
        public required string name { get; set; }
        public required string role { get; set; }
    }
}
