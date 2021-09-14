using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PK.OrdersWatcher.Shared.Models
{
    public class Order
    {
        /// <summary>
        /// gets or sets order no
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// gets or sets consignment no
        /// </summary>
        public string ConsignmentNo { get; set; }

        /// <summary>
        /// gets or sets parcel code
        /// </summary>
        public string ParcelCode { get; set; }

        /// <summary>
        /// gets or sets consignment name
        /// </summary>
        public string ConsigneeName { get; set; }

        /// <summary>
        /// gets or sets address of customer
        /// </summary>
        public Address Address { get; set; }

        /// <summary>
        /// gets or sets item qty
        /// </summary>
        public int ItemQuantity { get; set; }

        /// <summary>
        /// gets or sets item value
        /// </summary>
        public decimal ItemValue { get; set; }

        /// <summary>
        /// gets or sets item weight
        /// </summary>
        public decimal ItemWeight { get; set; }

        /// <summary>
        /// gets or sets item desc.
        /// </summary>
        public string ItemDescription { get; set; }

        /// <summary>
        /// gets or sets item currency
        /// </summary>
        public string ItemCurrency { get; set; }

    }
}
