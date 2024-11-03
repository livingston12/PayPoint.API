namespace PayPoint.Core.Models;

public class Ingredient
{
    public int IngredientId { get; set; }
    public string Name { get; set; }
    public decimal AdditionalPrice { get; set; }
    public bool IsOptional { get; set; }
    public int Quantity { get; set; }
}
