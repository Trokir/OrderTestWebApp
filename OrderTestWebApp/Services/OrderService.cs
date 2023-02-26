﻿using AutoMapper;

using Microsoft.EntityFrameworkCore;

using OrderTestWebApp.DTOs;
using OrderTestWebApp.EF;
using OrderTestWebApp.Enums;
using OrderTestWebApp.Models;
using OrderTestWebApp.Services.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderTestWebApp.Services
{
    public class OrderService : IOrderService
    {
        private readonly OrderDbContext _dbContext;
        private readonly IMapper _mapper;
        public OrderService(OrderDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task AddNewOrderAsync(OrderDTO order)
        {
            if (order is null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            var newOrder = new Order()
            {
                CreatedByUserName = order.CreatedByUserName,
                CreatedDate = order.CreatedDate,
                Type = order.Type,
                CustomerName = order.CustomerName
            };
            await _dbContext.Orders.AddAsync(newOrder);
            await _dbContext.SaveChangesAsync();

        }

        public async Task DeleteOrder(Guid id)
        {
            var isValid = Guid.TryParse(id.ToString(), out Guid guidOutput);
            if (isValid)
            {
                var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == guidOutput);
                if (order != null)
                {
                    _dbContext.Remove(order);
                    await _dbContext.SaveChangesAsync();
                }
            }
            else
            {
                throw new Exception(nameof(id));
            }
        }

        public async Task<IEnumerable<Order>> GetOrdersByOrderTypeAsync(OrderType orderType)
        {
            var ordersList = await _dbContext.Orders.Where(x => x.Type.Equals(orderType)).ToListAsync();
            return ordersList;
        }

        public async Task<IEnumerable<Order>> GetOrdersListAsync()
        {
            var ordersList = await _dbContext.Orders.ToListAsync();
            return ordersList;
        }

        public async Task<OrderDTO> UpdateOrderAsync(Order order)
        {
            if (order is null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            var currentOrder = await _dbContext.Orders.SingleOrDefaultAsync(x => x.Id == order.Id);
            if (currentOrder != null)
            {
                currentOrder.Type = order.Type;
                currentOrder.CreatedDate = order.CreatedDate;
                currentOrder.CreatedByUserName = order.CreatedByUserName;
                currentOrder.CustomerName = order.CustomerName;
                await _dbContext.SaveChangesAsync();
                var dto = _mapper.Map<OrderDTO>(order);
                return dto;
            }

            return default;
        }
    }
}