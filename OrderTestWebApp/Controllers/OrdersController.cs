using AutoMapper;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using OrderTestWebApp.DTOs;
using OrderTestWebApp.Enums;
using OrderTestWebApp.Helpers;
using OrderTestWebApp.Models;
using OrderTestWebApp.Services.Interfaces;
using OrderTestWebApp.Validator;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderTestWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly ILogger<Order> _logger;
        private readonly OrderInsertValidator _validationRulesForInsert;
        private readonly OrderUpdateValidator _validationRulesForUpdate;
        public OrdersController(
            IOrderService orderService,
            IMapper mapper,
            ILogger<Order> logger,
            OrderInsertValidator validationRulesForInsert,
            OrderUpdateValidator validationRulesForUpdate)
        {
            _mapper = mapper;
            _orderService = orderService;
            _logger = logger;
            _validationRulesForInsert = validationRulesForInsert;
            _validationRulesForUpdate = validationRulesForUpdate;
        }

        [HttpGet("getAll")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OrderDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrdersListAsync()
        {
            var listOfOrders = await _orderService.GetOrdersListAsync();
            var result = _mapper.Map<IEnumerable<OrderDTO>>(listOfOrders);
            _logger.LogDebug($"Received list of orders with {listOfOrders.Count()} items");
            return Ok(result);
        }
        [HttpGet("getOrderById")]
        public async Task<ActionResult<OrderDTO>> GetOrderByIdAsync(string id)
        {
            var orderModel = await _orderService.GetOrderByIdAsync(id);
            var orderDTO = _mapper.Map<OrderDTO>(orderModel);

            _logger.LogDebug($"Received   = {orderDTO};");
            return Ok(orderDTO);
        }


        [HttpGet("getOrdersByType")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrdersByOrderTypeAsync(string orderType)
        {
            var ordersList = await _orderService.GetOrdersByOrderTypeAsync(orderType);
            var result = _mapper.Map<IEnumerable<OrderDTO>>(ordersList);
            _logger.LogDebug($"Received  orders list with orderType = {orderType}; items count is {ordersList.Count()}");
            return Ok(result);
        }

        [HttpPost("addNew")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddNewOrderAsync(OrderInsertDTO order)
        {
            var validationResult = _validationRulesForInsert.Validate(order);
            if (validationResult.IsValid)
            {
                await _orderService.AddNewOrderAsync(order);
                _logger.LogDebug($"Added new  {order}");
                return Ok();
            }
            else
            {
                IEnumerable<Error> errors = validationResult.Errors.Select(e => new Error(e.ErrorCode, e.ErrorMessage));
                return BadRequest(errors);
            }
        }

        [HttpPut("updateOrder")]
        public async Task<ActionResult<OrderUpdateDTO>> UpdateOrderAsync([FromBody] OrderUpdateDTO order)
        {
            var orderDto = _mapper.Map<OrderUpdateDTO>(order);
           var validationResult = _validationRulesForUpdate.Validate(orderDto);
            if (validationResult.IsValid)
            {
                var result = await _orderService.UpdateOrderAsync(order);
                _logger.LogDebug($"Updated  {order}");
                return Ok(orderDto);
            }
            else
            {
                var errors = validationResult.Errors.Select(e => new Error(e.ErrorCode, e.ErrorMessage));
                return BadRequest(errors);
            }
        }

        [HttpDelete("remove")]
        public async Task<ActionResult> DeleteOrder(Guid id)
        {
            await _orderService.DeleteOrder(id);
            _logger.LogDebug($"Deleted order with id =  {id}");
            return Ok();
        }
    }
}
