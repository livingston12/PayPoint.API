using PayPoint.Core.DTOs.Rooms;
using PayPoint.Core.Models;

namespace PayPoint.Services.Interfaces;

public interface IRoomService
{
    Task<Room?> GetRoomByIdAsync(int RoomId);
    Task<IEnumerable<Room>> GetRoomsAsync();
    Task<Room?> AddRoomAsync(RoomCreateDto RoomCreateDto);
    Task<bool> UpdateRoomAsync(int id, RoomUpdateDto RoomUpdateDto);
    Task<bool> DeleteRoomAsync(int id);
}
