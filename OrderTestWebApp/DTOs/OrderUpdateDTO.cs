using OrderTestWebApp.Enums;

using System;

namespace OrderTestWebApp.DTOs
{
    public class OrderUpdateDTO
    {
        public Guid  Id { get; set; }
        public OrderType Type { get; set; }
        public string CustomerName { get; set; }
        public string CreatedByUserName { get; set; }

        public override string ToString()
        {
            return $"Order params: Type = {Type}; CustomerName = {CustomerName}; " +
                $" CreatedByUserName = {CreatedByUserName}";
        }
    }
}
