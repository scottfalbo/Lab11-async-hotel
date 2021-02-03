using AsyncHotel.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncHotel.Models.Interfaces
{
    public interface IHotel
    {
        Task<Hotel> Create(Hotel hotel);
        Task<HotelDto> GetHotel(int id);
        Task<List<HotelDto>> GetHotels();
        Task<Hotel> UpdateHotel(int id, Hotel hotel);
        Task DeleteHotel(int id);
    }
}
