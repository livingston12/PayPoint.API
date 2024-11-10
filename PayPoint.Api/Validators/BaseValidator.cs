using System.Linq.Expressions;
using FluentValidation;

namespace PayPoint.Api.Validators;

public class BaseValidator<TDto> : AbstractValidator<TDto>
{
    protected void RulesName(Expression<Func<TDto, string?>> propertyExpression)
    {
        RuleFor(propertyExpression)
            .NotEmpty().WithMessage("El nombre es obligatorio.")
            .MaximumLength(75).WithMessage("El nombre no puede exceder 75 caracteres.");
    }

    protected void RulesDescription(Expression<Func<TDto, string?>> propertyExpression)
    {
        RuleFor(propertyExpression)
            .MaximumLength(150).WithMessage("La descripcion no puede exceder 150 caracteres.");
    }

    protected void RulesPrice(Expression<Func<TDto, decimal?>> propertyExpression)
    {
        RuleFor(propertyExpression)
            .NotEmpty().WithMessage("El precio es obligatorio.")
            .GreaterThan(0).WithMessage("El precio debe ser mayor a 0.");
    }

    protected void RulesCategoryId(Expression<Func<TDto, int?>> propertyExpression)
    {
        RuleFor(propertyExpression)
            .GreaterThan(0).WithMessage("La categoria debe ser mayor a 0.");
    }

    protected void RulesSubCategoryId(Expression<Func<TDto, int?>> propertyExpression)
    {
        RuleFor(propertyExpression)
            .NotEmpty().WithMessage("La sub-categoria es obligatoria.")
            .GreaterThan(0).WithMessage("La sub-categoria debe ser mayor a 0.");
    }

    protected IRuleBuilderOptions<TDto, int?>? GreaterThan(Expression<Func<TDto, int?>> propertyExpression, int value)
    {
        return RuleFor(propertyExpression)
            .GreaterThan(value);
    }
}
