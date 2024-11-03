using System.Text.Json.Serialization;
using PayPoint.Core.DTOs.Categories;

namespace PayPoint.Core.DTOs;

public class SubCategoryDto
{
    public int SubCategoryId { get; set; }
    public string Name { get; set; }
    public int? CategoryId { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public CategoryDto? Category { get; set; }
}
