using PayPoint.Core.DTOs.SubCategories;

namespace PayPoint.Api.Validators;

public class SubCategoryUpdateDtoValidator : BaseValidator<SubCategoryUpdateDto>
{
    public SubCategoryUpdateDtoValidator()
    {
        RulesName(x => x.Name);
        RulesDescription(x => x.Description);
        RulesCategoryId(x => x.CategoryId);
    }
}
