using OrderTestWebApp.Enums;

using System;

namespace OrderTestWebApp.DTOs
{
    public class OrderInsertDTO
    {
        public string OrderType { get; set; }
        public string CustomerName { get; set; }
        public string CreatedByUserName { get; set; }

        public override string ToString()
        {
            return $"Order params: Type = {OrderType}; CustomerName = {CustomerName}; " +
                $" CreatedByUserName = {CreatedByUserName}";
        }
    }
}
