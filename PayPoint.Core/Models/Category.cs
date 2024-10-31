using PayPoint.Core.Enums;

namespace PayPoint.Core.Models;

public class Category
{
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public CategoryStatus Status { get; set; }

    public List<SubCategory> SubCategories { get; set; }
}
