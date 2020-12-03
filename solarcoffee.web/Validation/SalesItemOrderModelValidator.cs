using FluentValidation;
using solarcoffee.web.ViewModels;
using System.Linq;

public class SalesOrderModelValidator : AbstractValidator<SalesItemOrderModel>
{
    public SalesOrderModelValidator()
    {
    RuleFor(salesItem => salesItem.Id)
      .NotEmpty();


    RuleFor(salesItem => salesItem.Quantity)
        .NotEmpty();

  }
}