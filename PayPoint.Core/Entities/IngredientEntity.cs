using System.ComponentModel.DataAnnotations;

namespace PayPoint.Core.Entities;

public class IngredientEntity : BaseEntity
{
    [Key]
    public int IngredientId { get; set; }
    public string Name { get; set; }
    public decimal AdditionalPrice { get; set; }
    public bool IsOptional { get; set; }
    public int Quantity { get; set; }

    public ICollection<ProductIngredientEntity>? ProductIngredients { get; set; }
}
