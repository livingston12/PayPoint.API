namespace PayPoint.Core.Entities;

public class ProductIngredientEntity : BaseEntity
{
    public int ProductId { get; set; }
    public int IngredientId { get; set; }
    public int Quantity { get; set; }

    public ProductEntity Product { get; set; } = new();
    public IngredientEntity Ingredient { get; set; } = new();
}
