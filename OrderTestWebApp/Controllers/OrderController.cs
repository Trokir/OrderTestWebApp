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
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly ILogger<Order> _logger;
        private readonly OrderValidator _validator;
        private readonly OrderValidatorDTO _validatorDTO;
        public OrderController(
            IOrderService orderService,
            IMapper mapper,
            ILogger<Order> logger,
            OrderValidatorDTO validatorDTO,
            OrderValidator validator)
        {
            _mapper = mapper;
            _orderService = orderService;
            _logger = logger;
            _validatorDTO = validatorDTO;
            _validator = validator;
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

        [HttpGet("getOrdersByType")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrdersByOrderTypeAsync(OrderType orderType)
        {
            var ordersList = await _orderService.GetOrdersByOrderTypeAsync(orderType);
            var result = _mapper.Map<IEnumerable<OrderDTO>>(ordersList);
            _logger.LogDebug($"Received  orders list with orderType = {orderType}; items count is {ordersList.Count()}");
            return Ok(result);
        }

        [HttpPost("addNew")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddNewOrderAsync(OrderDTO order)
        {
            var validationResult = _validatorDTO.Validate(order);
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
        public async Task<ActionResult<OrderDTO>> UpdateOrderAsync([FromBody] Order order)
        {
            var orderDto = _mapper.Map<OrderDTO>(order);
           var validationResult = _validatorDTO.Validate(orderDto);
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
