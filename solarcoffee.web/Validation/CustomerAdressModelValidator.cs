using FluentValidation;
using solarcoffee.web.ViewModels;
using System.Linq;

public class CustomerAdressModelValidator : AbstractValidator<CustomerAdressModel>
{
    public CustomerAdressModelValidator()
    {
        RuleFor(Adress => Adress.State)
        .MinimumLength(2)
        .MaximumLength(20)
        .NotEmpty()
        .Must(name => name.Replace(" ", string.Empty).All(c => char.IsLetter(c)))
        .WithMessage("State cannot include numbers and symbols");

        RuleFor(Adress => Adress.AdressLine1)
        .MinimumLength(3)
        .MaximumLength(28)
        .NotEmpty()
        .Must(name => name.Replace(" ", string.Empty).Replace("/", string.Empty).All(c => char.IsLetterOrDigit(c)))
        .WithMessage("Adress cannot include symbols");

        RuleFor(Adress => Adress.AdressLine2)
        .MinimumLength(3)
        .MaximumLength(28)
        .NotEmpty()
     .Must(name => name.Replace(" ", string.Empty).Replace("/", string.Empty).All(c => char.IsLetterOrDigit(c)))
        .WithMessage("Adress cannot include symbols");

        RuleFor(Adress => Adress.PostalCode)
        .MinimumLength(3)
        .MaximumLength(28)
        .NotEmpty()
        .Must(name => name.Replace(" ", string.Empty).Replace("-", string.Empty).All(c => char.IsDigit(c)))
        .WithMessage("Postal code must be numbers and - only");

   






    }
}