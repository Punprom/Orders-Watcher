using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PK.OrdersWatcher.Shared.Models
{
    /// <summary>
    /// Address: customer address class
    /// </summary>
    public class Address
    {
        /// <summary>
        /// gets or sets address 1
        /// </summary>
        public string Address1 { get; set; }

        /// <summary>
        /// gets or sets address 2
        /// </summary>
        public string Address2 { get; set; }

        /// <summary>
        /// gets or sets city
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// gets or sets state
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// gets or sets country code
        /// </summary>
        public string CountryCode { get; set; }
    }
}
