using AsyncHotel.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncHotel.Models.Interfaces
{
    public interface IRoom
    {
        Task<Room> Create(RoomDto room);
        Task<RoomDto> GetRoom(int id);
        Task<List<RoomDto>> GetRooms();
        Task<Room> UpdateRoom(int id, RoomDto room);
        Task DeleteRoom(int id);
        Task AddAmenityToRoom(int roomId, int amenitiesId);
        Task RemoveAmenityFromRoom(int roomId, int amenitiesId);
    }
}
