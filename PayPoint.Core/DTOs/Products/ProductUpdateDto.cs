using PayPoint.Core.Enums;

namespace PayPoint.Core.DTOs.Products;

public class ProductUpdateDto
{
    public string? Name { get; set; }
    public string? ImageUrl { get; set; }
    public decimal? Price { get; set; }
    public Currencies? Currency { get; set; } = Currencies.DOP;
    public int? SubCategoryId { get; set; }

    public IEnumerable<int>? IngredientIds { get; set; }
}
