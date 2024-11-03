using FluentValidation;
using PayPoint.Core.DTOs.Ingredients;

namespace PayPoint.Api.Validators;

public class IngredientCreateDtoValidator : BaseValidator<IngredientCreateDto>
{
    public IngredientCreateDtoValidator()
    {
        RulesName(x => x.Name);
        RuleFor(x => x.AdditionalPrice)
            .GreaterThan(0).WithMessage("El precio adicional debe ser mayor a 0.");
    }
}
