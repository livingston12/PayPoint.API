using PayPoint.Core.Enums;

namespace PayPoint.Core.Entities;

public class ReservationEntity : BaseEntity
{
    public int ReservationId { get; set; }
    public int? ClientId { get; set; }
    public int TableId { get; set; }
    public DateTime ReservationDate { get; set; } = DateTime.UtcNow;
    public int NumberOfPeople { get; set; }
    public ReservationStatus Status { get; set; }
}
