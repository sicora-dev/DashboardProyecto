using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardTienda.Models
{
    public class UserLogin
    {
        public required string mail { get; set; }
        public required string password { get; set; }
    }
}

