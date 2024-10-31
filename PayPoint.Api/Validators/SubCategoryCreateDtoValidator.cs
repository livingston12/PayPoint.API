using PayPoint.Core.DTOs.SubCategories;

namespace PayPoint.Api.Validators;

public class SubCategoryCreateDtoValidator : BaseValidator<SubCategoryCreateDto>
{
    public SubCategoryCreateDtoValidator()
    {
        RulesName(x => x.Name);
        RulesDescription(x => x.Description);
        RulesCategoryId(x => x.CategoryId);
    }
}
