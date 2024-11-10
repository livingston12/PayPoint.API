using PayPoint.Core.Enums;

namespace PayPoint.Core.Models;

public class Table
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public int TableId { get; set; }
    public int TableNumber { get; set; }
    public int Capacity { get; set; }
    public int RoomId { get; set; }
    public TableStatus Status { get; set; }
}
