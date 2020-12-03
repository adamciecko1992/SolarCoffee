using FluentValidation;
using solarcoffee.web.ViewModels;
using System.Linq;

public class OrderModelValidator : AbstractValidator<OrderModel>
{
    public OrderModelValidator()
    {
    RuleFor(Order => Order.CreatedOn)
       .NotEmpty();
       

    RuleFor(Order => Order.UpdatedOn)
       .NotEmpty();

    RuleFor(Order => Order.LineItems)
      .Must(orderList => orderList.All(v => new SalesOrderModelValidator().Validate(v).IsValid));

    RuleFor(Order => Order.Customer)
   .SetValidator(customer => new CustomerModelValidator());

    RuleFor(Order => Order.IsPaid)
    .NotEmpty()
    .Equals(true || false);

    RuleFor(Order => Order.Id)
    .NotEmpty();

  }
}