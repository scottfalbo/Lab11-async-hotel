using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncHotel.Models.Interfaces
{
    public interface IRoom
    {
        Task<Room> Create(Room room);
        Task<Room> GetRoom(int id);
        Task<List<Room>> GetRooms();
        Task<Room> UpdateRoom(int id, Room room);
        Task DeleteRoom(int id);
    }
}
