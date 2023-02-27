using FluentValidation;

using OrderTestWebApp.Models;

using System;

namespace OrderTestWebApp.Validator
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(model => model.Id).NotNull().WithMessage("Id must be not empty");
            RuleFor(model => model.CustomerName).NotEmpty().Length(3, 20).WithMessage("The customer name is invalid");
            RuleFor(model => model.CreatedByUserName).NotEmpty().Length(3, 20).WithMessage("The CreatedByUserName name is invalid");
            RuleFor(model => model.CreatedDate).GreaterThan(p => DateTime.Now.AddHours(-1)).LessThan(p => DateTime.Now).WithMessage("Invalid Date format");
            RuleFor(model => model.Type).IsInEnum().WithMessage("Must be enum");
        }
    }
}
