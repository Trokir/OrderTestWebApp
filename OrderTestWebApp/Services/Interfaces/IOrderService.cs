using OrderTestWebApp.DTOs;
using OrderTestWebApp.Enums;
using OrderTestWebApp.Models;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderTestWebApp.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetOrdersListAsync();
        Task<IEnumerable<Order>> GetOrdersByOrderTypeAsync(OrderType type);
        Task<OrderDTO> UpdateOrderAsync(Order order);
        Task DeleteOrder(Guid id);
        Task AddNewOrderAsync(OrderDTO order);
    }
}
