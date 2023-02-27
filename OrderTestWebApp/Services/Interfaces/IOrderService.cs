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
        Task<Order> GetOrderByIdAsync(string id);
        Task<IEnumerable<Order>> GetOrdersByOrderTypeAsync(string type);
        Task<OrderUpdateDTO> UpdateOrderAsync(OrderUpdateDTO order);
        Task DeleteOrder(Guid id);
        Task AddNewOrderAsync(OrderInsertDTO order);
    }
}
