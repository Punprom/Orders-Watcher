namespace PK.OrdersWatcher.Shared.Dtos
{
    /// <summary>
    /// Order data object
    /// </summary>
    public class OrderDto
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
