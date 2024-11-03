namespace PayPoint.Core.DTOs.Ingredients;

public class IngredientUpdateDto
{
    public string? Name { get; set; }
    public decimal? AdditionalPrice { get; set; }
    public bool? IsOptional { get; set; }
}
