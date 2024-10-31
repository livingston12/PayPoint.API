namespace PayPoint.Core.Entities;

public class RoomEntity : BaseEntity
{
    public int RoomId { get; set; }
    public string Nombre { get; set; }
    public string? Description { get; set; }
    public int Capacity { get; set; }
    
    public ICollection<TableEntity> Tables { get; set; } = new List<TableEntity>();
}
