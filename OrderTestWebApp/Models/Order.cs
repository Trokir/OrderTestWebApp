using OrderTestWebApp.Enums;

using System;

namespace OrderTestWebApp.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public OrderType OrderType { get; set; }
        public string CustomerName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedByUserName { get; set; }
    }
}
