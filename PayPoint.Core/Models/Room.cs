using PayPoint.Core.DTOs.Tables;

namespace PayPoint.Core.Models;

public class Room
{
    public int RoomId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public int? Capacity { get; set; }
    
    public IEnumerable<TableDto>? Tables { get; set; }
}
