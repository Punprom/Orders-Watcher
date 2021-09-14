using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PK.OrdersWatcher.Shared.Models
{
    public class OrderSummary
    {
        public decimal Quantity { get; set; }

        public decimal Weight { get; set; }

        public decimal Total { get; set; }
    }
}
