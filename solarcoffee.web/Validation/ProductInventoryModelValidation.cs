using FluentValidation;
using solarcoffee.web.ViewModels;
using System.Linq;

public class ProductInventoryModelValidator : AbstractValidator<ProductInventoryModel>
{
    public ProductInventoryModelValidator()
    {
    RuleFor(inventory => inventory.Id)
    .NotEmpty();


    RuleFor(inventory => inventory.Product)
    .SetValidator(product => new ProductModelValidator());






  }
}