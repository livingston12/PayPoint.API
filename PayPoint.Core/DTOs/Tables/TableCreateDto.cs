using PayPoint.Core.Enums;

namespace PayPoint.Core.DTOs.Tables;

public class TableCreateDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int? TableNumber { get; set; }
    public int? Capacity { get; set; }
    public int? RoomId { get; set; }
    public TableStatus? Status { get; set; }
}
