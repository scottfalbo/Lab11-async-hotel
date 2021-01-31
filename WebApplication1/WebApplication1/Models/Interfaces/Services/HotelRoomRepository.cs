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
                                                           .ThenInclude(x => x.RoomAmenities)
                                                           .ThenInclude(x => x.Amenities)
                                                           .FirstOrDefaultAsync();
            return hotelRoom;
        }

        public async Task<List<HotelRoom>> GetHotelRooms(int hotelId)
        {
            List<HotelRoom> hotelRooms = await _context.HotelRooms.Where(x => x.HotelId == hotelId)
                                                                  .Include(x => x.Room)
                                                                  .ThenInclude(x => x.RoomAmenities)
                                                                  .ThenInclude(x => x.Amenities)
                                                                  .ToListAsync();
            return hotelRooms;
        }

        public async Task<HotelRoom> UpdateHotelRoom(HotelRoom hotelRoom)
        {
            _context.Entry(hotelRoom).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return hotelRoom;
        }

        public async Task DeleteHotelRoom(int hotelId, int roomId)
        {
            HotelRoom hotelRoom = await GetHotelRoom(hotelId, roomId);
            _context.Entry(hotelRoom).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

    }
}
