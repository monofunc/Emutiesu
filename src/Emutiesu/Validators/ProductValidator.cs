using Emutiesu.Models;
using FluentValidation;

namespace Emutiesu.Validators;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(x => x.Type).NotEmpty();
        RuleFor(x => x.PrimaryCategory).NotEmpty();
        RuleFor(x => x.SecondaryCategory).NotEmpty();
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.Price).GreaterThan(0);
    }
}
