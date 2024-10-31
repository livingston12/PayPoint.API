using PayPoint.Core.Enums;

namespace PayPoint.Core.Entities;

public class SubCategoryEntity
{
    public int SubCategoryId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public CategoryStatus Status { get; set; }
    public int? CategoryId { get; set; }

    public CategoryEntity? Category { get; set; }
    public ICollection<ProductEntity> Products { get; set; } = new List<ProductEntity>();
}
