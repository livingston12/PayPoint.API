using PayPoint.Core.DTOs.Categories;

namespace PayPoint.Api.Validators;

public class CategoryUpdateDtoValidator : BaseValidator<CategoryUpdateDto>
{
    public CategoryUpdateDtoValidator()
    {
        RulesName(x => x.Name);
        RulesDescription(x => x.Description);
    }
}
