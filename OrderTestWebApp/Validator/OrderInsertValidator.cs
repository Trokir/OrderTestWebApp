using FluentValidation;

using OrderTestWebApp.DTOs;

namespace OrderTestWebApp.Validator
{
    public class OrderInsertValidator : AbstractValidator<OrderInsertDTO>
    {
        public OrderInsertValidator()
        {
            RuleFor(model => model.CustomerName).NotEmpty().Length(3, 20).WithMessage("The customer name is invalid");
            RuleFor(model => model.CreatedByUserName).NotEmpty().Length(3, 20).WithMessage("The CreatedByUserName name is invalid");
            RuleFor(model => model.OrderType).NotEmpty().WithMessage("Must be enum");
        }
    }
    public class OrderUpdateValidator : AbstractValidator<OrderUpdateDTO>
    {
        public OrderUpdateValidator()
        {
            RuleFor(model => model.CustomerName).NotEmpty().Length(3, 20).WithMessage("The customer name is invalid");
            RuleFor(model => model.CreatedByUserName).NotEmpty().Length(3, 20).WithMessage("The CreatedByUserName name is invalid");
            RuleFor(model => model.OrderType).IsInEnum().WithMessage("Must be enum");
        }
    }
}
