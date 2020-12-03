using FluentValidation;
using solarcoffee.web.ViewModels;
using System.Linq;

public class InvoiceModelValidator : AbstractValidator<InvoiceModel>
{
    public InvoiceModelValidator()
    {
    RuleFor(Adress => Adress.CreatedOn)
      .NotEmpty();
    RuleFor(Adress => Adress.UpdatedOn)
      .NotEmpty();
    RuleFor(Adress => Adress.CustomerId)
      .NotEmpty();

    RuleFor(Adress => Adress.LineItems)
      .NotEmpty();
   }
}