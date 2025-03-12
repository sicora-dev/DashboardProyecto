using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardTienda.Models
{
    public class User
    {

        public required string _id { get; set; }
        public required string name { get; set; }
        public required string mail { get; set; }
        public required int city_id { get; set; }
        public required int country_id { get; set; }
        public required bool blocked { get; set; }
        public required string block_date { get; set; }
        public required string role { get; set; }
    }
}
