using PayPoint.Core.DTOs.Categories;

namespace PayPoint.Api.Validators;

public class CategoryCreateDtoValidator : BaseValidator<CategoryCreateDto>
{
    public CategoryCreateDtoValidator()
    {
        RulesName(x => x.Name);
        RulesDescription(x => x.Description);
    }
}
