using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardTienda.Models
{
    public class City
    {

        public required string _id { get; set; }
        public required string name { get; set; }
        public required int county_id { get; set; }
    }
}
