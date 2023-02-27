using OrderTestWebApp.Models;
using OrderTestWebApp.Validator;

using System;

using Xunit;

namespace OrderTestWebApp.Tests
{
    public class ValidateOrdersModelTests
    {
        private readonly OrderValidator _validator;
        public ValidateOrdersModelTests()
        {
            _validator = new OrderValidator();
        }
        [Fact]
        public void NotAllowEmptyCustomerNameThenThrowBadRequest()
        {
            Order order = new()
            {
                Id = Guid.NewGuid(),
                OrderType = Enums.OrderType.SaleOrder,
                CustomerName = string.Empty,
                CreatedByUserName = "John",
                CreatedDate = DateTime.Now
            };
            var result = _validator.Validate(order);
            Assert.Contains(result.Errors, o => o.PropertyName == "CustomerName");
        }
        [Fact]
        public void NotAllowShortCustomerNameThenThrowBadRequest()
        {
            Order order = new()
            {
                Id = Guid.NewGuid(),
                OrderType = Enums.OrderType.SaleOrder,
                CustomerName = "s",
                CreatedByUserName = "John",
                CreatedDate = DateTime.Now
            };
            var result = _validator.Validate(order);
            Assert.Contains(result.Errors, o => o.PropertyName == "CustomerName");
        }
        [Fact]
        public void NotAllowLongCustomerNameThenThrowBadRequest()
        {
            Order order = new()
            {
                Id = Guid.NewGuid(),
                OrderType = Enums.OrderType.SaleOrder,
                CustomerName = "sdddddddddddddddddddddddddddddddddddd" + "dddddddddddddddddddddddddddddddddddddd" + "ddddddddddddddddddddddddddddddddddddd" + "dddddddddddddddddddddddddddddddddddddd" + "dddddddddddddddddddddddddddddddddddddddd" + "ddddddddddddddddddddddddddddddddddddddddddd" + "dddddddddddddddddddddddddddddddddddddddddddddd" + "dddddddddddddddddddddddddddddddddddddddddddd" + "ddddddddddddddddddddddddddddddddddddddddddddd" + "",
                CreatedByUserName = "John",
                CreatedDate = DateTime.Now
            };
            var result = _validator.Validate(order);
            Assert.Contains(result.Errors, o => o.PropertyName == "CustomerName");
        }
        [Fact]
        public void NotAllowShortCreatedByUserNameThenThrowBadRequest()
        {
            Order order = new()
            {
                Id = Guid.NewGuid(),
                OrderType = Enums.OrderType.SaleOrder,
                CustomerName = "Bill",
                CreatedByUserName = "s",
                CreatedDate = DateTime.Now
            };
            var result = _validator.Validate(order);
            Assert.Contains(result.Errors, o => o.PropertyName == "CreatedByUserName");
        }
        [Fact]
        public void NotAllowLongCreatedByUserNameThenThrowBadRequest()
        {
            Order order = new()
            {
                Id = Guid.NewGuid(),
                OrderType = Enums.OrderType.SaleOrder,
                CreatedByUserName = "sdddddddddddddddddddddddddddddddddddd" + "dddddddddddddddddddddddddddddddddddddd" + "ddddddddddddddddddddddddddddddddddddd" + "dddddddddddddddddddddddddddddddddddddd" + "dddddddddddddddddddddddddddddddddddddddd" + "ddddddddddddddddddddddddddddddddddddddddddd" + "dddddddddddddddddddddddddddddddddddddddddddddd" + "dddddddddddddddddddddddddddddddddddddddddddd" + "ddddddddddddddddddddddddddddddddddddddddddddd" + "",
                CustomerName = "John",
                CreatedDate = DateTime.Now
            };
            var result = _validator.Validate(order);
            Assert.Contains(result.Errors, o => o.PropertyName == "CreatedByUserName");
        }
        [Fact]
        public void NotAllowWhenDateIsLessThanNowThenThrowBadRequest()
        {
            Order order = new()
            {
                Id = Guid.NewGuid(),
                OrderType = Enums.OrderType.SaleOrder,
                CreatedByUserName = "Bill",
                CreatedDate = DateTime.Now.AddDays(1)
            };
            var result = _validator.Validate(order);
            Assert.Contains(result.Errors, o => o.PropertyName == "CreatedDate");
        }
        [Fact]
        public void NotAllowWhenDateIsGreaterThanNowThenThrowBadRequest()
        {
            Order order = new()
            {
                Id = Guid.NewGuid(),
                OrderType = Enums.OrderType.SaleOrder,
                CreatedByUserName = "Bill",
                CreatedDate = DateTime.Now.AddDays(-1)
            };
            var result = _validator.Validate(order);
            Assert.Contains(result.Errors, o => o.PropertyName == "CreatedDate");
        }
    }
}
