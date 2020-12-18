using FluentValidation;
using solarcoffee.web.ViewModels;
using System.Linq;

public class ShipmentModelValidator : AbstractValidator<ShipmentModel>
{
    public ShipmentModelValidator()
    {
    RuleFor(shpiment => shpiment.IdProduct)
    .NotEmpty();

     RuleFor(shpiment => shpiment.ProductAdjustment)
    .NotEmpty();
  }
}