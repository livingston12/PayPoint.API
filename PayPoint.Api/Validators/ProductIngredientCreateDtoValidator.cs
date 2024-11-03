using FluentValidation;
using PayPoint.Core.DTOs.Products;

namespace PayPoint.Api.Validators;

public class ProductIngredientCreateDtoValidator : BaseValidator<ProductIngredientCreateDto>
{
    public ProductIngredientCreateDtoValidator()
    {
        RuleFor(x => x.IngredientId)
            .NotEmpty().WithMessage("El ingrediente es obligatorio.")
            .GreaterThan(0).WithMessage("El ingrediente debe ser mayor a 0.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("La cantidad debe ser mayor a 0."); 
    }
}
