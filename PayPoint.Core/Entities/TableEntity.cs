using PayPoint.Core.Enums;

namespace PayPoint.Core.Entities;

public class TableEntity : BaseEntity
{
    public int TableId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public int TableNumber { get; set; }
    public int Capacity { get; set; }
    public int RoomId { get; set; }
    public TableStatus Status { get; set; }

    public RoomEntity? Room { get; set; }
    public ICollection<OrderEntity>? Orders { get; set; }
}
