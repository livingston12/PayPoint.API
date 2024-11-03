using FluentValidation;
using PayPoint.Core.DTOs.Ingredients;

namespace PayPoint.Api.Validators;

public class IngredientUpdateDtoValidator : BaseValidator<IngredientUpdateDto>
{
    public IngredientUpdateDtoValidator()
    {
        RulesName(x => x.Name);
        RuleFor(x => x.AdditionalPrice)
             .GreaterThan(0).WithMessage("El precio adicional debe ser mayor a 0.");
    }
}
