namespace PayPoint.Core.Entities;

public class OrderStatusEntity : BaseEntity
{
    public int OrderStatusId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }

    public ICollection<OrderEntity> Orders { get; set; } = new List<OrderEntity>();
}
