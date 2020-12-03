using System;
using FluentValidation;
using solarcoffee.web.ViewModels;
using System.Linq;

public class CustomerModelValidator : AbstractValidator<CustomerModel>
{
    public CustomerModelValidator()
    {

        RuleFor(Customer => Customer.FirstName)
        .MinimumLength(3)
        .MaximumLength(20)
        .NotEmpty()
        .Must(name => name.All(c => char.IsLetter(c)))
        .WithMessage("First name must be valid");

        RuleFor(Customer => Customer.LastName)
        .MinimumLength(3)
        .MaximumLength(28)
        .NotEmpty()
        .Must(name => name.All(c => char.IsLetter(c)))
        .WithMessage("Last name must be valid");


        RuleFor(Customer => Customer.PrimaryAdress)
          .SetValidator(v => new CustomerAdressModelValidator())
          .WithMessage("Error on validating primary adress");
    }
}