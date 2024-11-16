namespace PayPoint.Core.DTOs.Products;
public class ProductDto
{
    public bool? IncludeAll { get; set; }
    public bool? IncludeCategory { get; set; }
    public bool? IncludeSubCategory { get; set; } = false;
    public bool? IncludeIngredient { get; set; }
}