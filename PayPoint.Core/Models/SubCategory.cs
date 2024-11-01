using System.Text.Json.Serialization;
using PayPoint.Core.Enums;

namespace PayPoint.Core.Models;

public class SubCategory
{
    public int SubCategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public CategoryStatus Status { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public SubCategoryCategory? Category { get; set; }
}
