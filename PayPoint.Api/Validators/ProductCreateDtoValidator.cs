using PayPoint.Core.DTOs.Products;

namespace PayPoint.Api.Validators;

public class ProductCreateDtoValidator : BaseValidator<ProductCreateDto>
{
    public ProductCreateDtoValidator()
    {
        RulesName(x => x.Name);
        RulesPrice(x => x.Price);

        RulesSubCategoryId(x => x.SubCategoryId);
    }
}
