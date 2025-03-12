using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardTienda.Models
{
    public class Comment
    {
        public string _id { get; set; }
        public string user_id { get; set; }
        public string name { get; set; }
        public string product_id { get; set; }
        public string content { get; set; }
        public DateTime date { get; set; }
    }
}
