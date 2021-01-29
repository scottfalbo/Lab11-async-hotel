using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncHotel.Models.Interfaces
{
    interface IHotelRoom
    {
        Task<HotelRoom> Create(HotelRoom hotelRoom);
        Task<HotelRoom> GetHotelRoom(int id);
        Task<List<HotelRoom>> GetHotelRooms();
        Task<HotelRoom> UpdateHotelRoom(int id, HotelRoom hoteRoom);
        Task DeleteHotelRoom(int id);
    }
}
