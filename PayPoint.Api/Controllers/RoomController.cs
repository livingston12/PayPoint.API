using Microsoft.AspNetCore.Mvc;
using PayPoint.Core.DTOs.Rooms;
using PayPoint.Core.Extensions;
using PayPoint.Core.Models;
using PayPoint.Services.Interfaces;

namespace PayPoint.Api.Controllers;

public class RoomController : BaseController
{
    private readonly IRoomService _roomService;

    public RoomController(IRoomService roomService)
    {
        _roomService = roomService;
    }

    [HttpGet]
    public async Task<IActionResult> GetRooms()
    {
        IEnumerable<Room> rooms = await _roomService.GetRoomsAsync();
        return Ok(rooms);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRoomById(int id)
    {
        Room? room = await _roomService.GetRoomByIdAsync(id);

        if (room.IsNullOrEmpty())
        {
            return NotFound();
        }
        return Ok(room);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRoom(int id)
    {
        bool isDeleted = await _roomService.DeleteRoomAsync(id);
        if (!isDeleted)
        {
            return BadRequest("Error inesperado: intente de nuevo o contacte con el administrador.");
        }
        
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRoom(int id, [FromBody] RoomUpdateDto roomUpdateDto)
    {
        bool isUpdated = await _roomService.UpdateRoomAsync(id, roomUpdateDto);
        if (!isUpdated)
        {
            return BadRequest("Error inesperado: intente de nuevo o contacte con el administrador.");
        }
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> AddRoom([FromBody] RoomCreateDto roomCreateDto)
    {
        Room? room = await _roomService.AddRoomAsync(roomCreateDto);
        if (room.IsNullOrEmpty())
        {
            return BadRequest("Error inesperado: intente de nuevo o contacte con el administrador.");
        }
        return CreatedAtAction(nameof(GetRoomById), new { id = room!.RoomId }, room);
    }
}
