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
        public async Task<HotelRoom> Create(HotelRoom hotelRoom)
        {
            _context.Entry(hotelRoom).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return hotelRoom;
        }

        public async Task<HotelRoom> GetHotelRoom(int id)
        {
            HotelRoom hotelRoom = await _context.HotelRooms.FindAsync(id);
            return hotelRoom;
        }

        public async Task<List<HotelRoom>> GetHotelRooms()
        {
            var hotelRooms = await _context.HotelRooms.ToListAsync();
            return hotelRooms;
        }

        public async Task<HotelRoom> UpdateHotelRoom(int id, HotelRoom hotelRoom)
        {
            _context.Entry(hotelRoom).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return hotelRoom;
        }
        public async Task DeleteHotelRoom(int id)
        {
            HotelRoom hotelRoom = await GetHotelRoom(id);
            _context.Entry(hotelRoom).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
