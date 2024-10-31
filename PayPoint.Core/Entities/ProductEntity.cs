using System.ComponentModel.DataAnnotations;
using PayPoint.Core.Enums;
using PayPoint.Core.Interfaces;

namespace PayPoint.Core.Entities;

public class ProductEntity : BaseEntity, IEntity
{
    public int Id => ProductId;

    public int ProductId { get; set; }
    public string Name { get; private set; }
    public string? ImageUrl { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public Currencies Currency { get; set; }
    public int SubCategoryId { get; set; }
    public ProductStatus Status { get; set; }
    public bool HasIngredients { get; set; }

    public ICollection<ProductIngredientEntity>? ProductIngredients { get; set; }
    public SubCategoryEntity SubCategory { get; set; } = new();
}
