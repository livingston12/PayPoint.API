using System.Text.Json.Serialization;
using PayPoint.Core.DTOs;
using PayPoint.Core.DTOs.Ingredients;
using PayPoint.Core.Enums;

namespace PayPoint.Core.Models;

public class Product
{
    public int ProductId { get; set; }
    public string Name { get; private set; }
    public string? ImageUrl { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public Currencies Currency { get; set; }
    public int SubCategoryId { get; set; }
    public ProductStatus Status { get; set; }
    public bool HasIngredients { get; set; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<IngredientDto>? Ingredients { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public SubCategoryDto? SubCategory { get; set; }
}
