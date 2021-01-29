using AsyncHotel.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncHotel.Models.Interfaces.Services
{
    public class HotelRoomRepository : IHotelRoom
    {
        private readonly AsyncDbContext _context;

        public HotelRoomRepository(AsyncDbContext context)
        {
            _context = context;
        }

        public async Task<HotelRoom> Create(HotelRoom hotelRoom, int hotelId)
        {
            _context.Entry(hotelRoom).State = EntityState.Added;
            await _context.SaveChangesAsync();

            return hotelRoom;
        }


        public async Task<HotelRoom> GetHotelRoom(int hotelId, int roomId)
        {
            HotelRoom hotelRoom = await _context.HotelRooms.Where(x => x.HotelId == hotelId && x.RoomId == roomId)
                                                           .Include(x => x.Hotel)
                                                           .Include(x => x.Room)
                                                           .FirstOrDefaultAsync();
            return hotelRoom;
        }

        public async Task<List<HotelRoom>> GetHotelRooms(int hotelId)
        {
            List<HotelRoom> hotelRooms = await _context.HotelRooms.Where(x => x.HotelId == hotelId)
                                                                  .Include(x => x.Room)
                                                                  .ToListAsync();
            return hotelRooms;
        }

        public Task<HotelRoom> UpdateHotelRoom(HotelRoom hotelRoom)
        {
            throw new NotImplementedException();
        }

        public Task DeleteHotelRoom(int hotelId, int roomId)
        {
            throw new NotImplementedException();
        }

    }
}
