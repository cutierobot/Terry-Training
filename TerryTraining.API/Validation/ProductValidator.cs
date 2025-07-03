using FluentValidation;
using TerryTraining.Application.DTO;

namespace TerryTraining.API.Validation;

// Medium article I followed for validation
// https://medium.com/@lucas.and227/fluent-validation-with-net-core-da0d9da73c8a

public class ProductValidator: AbstractValidator<ProductDTO>
{
    public ProductValidator()
    {
        RuleFor(product => product.Name)
            .NotEmpty()
            .MaximumLength(200)
            .WithMessage("Name must be not empty and no bigger then 200 characters.");

        RuleFor(product => product.Description)
            .MaximumLength(2000)
            .WithMessage("Description can not be bigger then 2,000 characters.");
    }
}