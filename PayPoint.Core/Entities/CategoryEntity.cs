using PayPoint.Core.Enums;
using PayPoint.Core.Interfaces;

namespace PayPoint.Core.Entities;

public class CategoryEntity : BaseEntity, IEntity
{
    public int Id => CategoryId;

    public int CategoryId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public CategoryStatus Status { get; set; }

    public ICollection<SubCategoryEntity> SubCategories { get; set; } = new List<SubCategoryEntity>();
}
