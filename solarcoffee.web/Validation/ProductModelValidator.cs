using FluentValidation;
using solarcoffee.web.ViewModels;
using System.Linq;

public class ProductModelValidator : AbstractValidator<ProductModel>
{
    public ProductModelValidator()
    {
    RuleFor(Product => Product.CreatedOn)
    .NotEmpty();


    RuleFor(Product => Product.UpdatedOn)
        .NotEmpty();


    RuleFor(Product => Product.Description)
        .NotEmpty()
        .Must(description => description.Replace(" ", string.Empty).All(c => char.IsLetterOrDigit(c)))
        .MinimumLength(5)
        .MaximumLength(80);
  }
}