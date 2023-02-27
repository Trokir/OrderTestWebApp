using FluentValidation;

using OrderTestWebApp.DTOs;

using System;

namespace OrderTestWebApp.Validator
{
    public class OrderValidatorDTO : AbstractValidator<OrderDTO>
    {
        public OrderValidatorDTO()
        {
            RuleFor(model => model.CustomerName).NotEmpty().Length(3, 100).WithMessage("The customer name is invalid");
            RuleFor(model => model.CreatedByUserName).NotEmpty().Length(3, 100).WithMessage("The CreatedByUserName name is invalid");
            RuleFor(model => model.CreatedDate).GreaterThan(p => DateTime.Now.AddHours(-1)).LessThan(p => DateTime.Now).WithMessage("Invalid Date format");
            RuleFor(model => model.Type).NotEmpty().Length(3, 100).WithMessage("Must be enum");
        }
    }
}
