using FluentValidation;
using PayPoint.Core.DTOs.Products;

namespace PayPoint.Api.Validators;

public class ProductUpdateDtoValidator : BaseValidator<ProductUpdateDto>
{
    public ProductUpdateDtoValidator()
    {
        RulesName(x => x.Name);

        RulesPrice(x => x.Price);

        RulesSubCategoryId(x => x.SubCategoryId);
    }
}
