using AutoMapper;
using PayPoint.Core.DTOs.Rooms;
using PayPoint.Core.Entities;
using PayPoint.Core.Extensions;
using PayPoint.Core.Interfaces;
using PayPoint.Core.Models;
using PayPoint.Services.Interfaces;

namespace PayPoint.Services.Implementations;

public class RoomService : BaseService, IRoomService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public RoomService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Room?> AddRoomAsync(RoomCreateDto RoomCreateDto)
    {
        RoomEntity roomEntity = _mapper.Map<RoomEntity>(RoomCreateDto);

        await _unitOfWork.Rooms.AddAsync(roomEntity);
        int? rowsInserted = await _unitOfWork.SaveChangesAsync();

        if (rowsInserted.IsLessThanOrEqualTo(0))
        {
            return null;
        }

        return _mapper.Map<Room>(roomEntity);
    }

    public async Task<bool> DeleteRoomAsync(int id)
    {
        _ = await GetRoomById(id);

        await _unitOfWork.Rooms.DeleteAsync(id);
        int? rowsDeleted = await _unitOfWork.SaveChangesAsync();

        return rowsDeleted.IsGreaterThan(0);
    }

    public async Task<Room?> GetRoomByIdAsync(int RoomId)
    {
        RoomEntity? roomEntity = await _unitOfWork.Rooms.GetByIdAsync(RoomId);

        if (roomEntity == null)
        {
            return null;
        }

        return _mapper.Map<Room>(roomEntity);
    }

    public async Task<IEnumerable<Room>> GetRoomsAsync()
    {
        IEnumerable<RoomEntity>? RoomsEntity = await _unitOfWork.Rooms.GetAllAsync();

        return _mapper.Map<IEnumerable<Room>>(RoomsEntity);
    }

    public async Task<bool> UpdateRoomAsync(int id, RoomUpdateDto RoomUpdateDto)
    {
        RoomEntity? roomEntity = await GetRoomById(id);

        if (roomEntity.IsNullOrEmpty())
        {
            return false;
        }

        roomEntity = _mapper.Map(RoomUpdateDto, roomEntity);

        _unitOfWork.Rooms.Update(roomEntity!);
        int? rowsUpdated = await _unitOfWork.SaveChangesAsync();

        return rowsUpdated.IsGreaterThan(0);
    }
}
