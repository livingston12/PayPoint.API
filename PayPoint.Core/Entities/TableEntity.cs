using PayPoint.Core.Enums;

namespace PayPoint.Core.Entities;

public class TableEntity : BaseEntity
{
    public int TableId { get; set; }
    public int TableNumber { get; set; }
    public int Capacity { get; set; }
    public int FloorId { get; set; }
    public TableStatus Status { get; set; }

    public RoomEntity Floor { get; set; } = new RoomEntity();
}
