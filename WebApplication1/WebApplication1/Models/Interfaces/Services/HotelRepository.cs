using AsyncHotel.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncHotel.Models.Interfaces.Services
{
    public class HotelRepository : IHotel
    {
        private readonly AsyncDbContext _context;

        public HotelRepository(AsyncDbContext context)
        {
            _context = context;
        }

        public async Task<Hotel> Create(Hotel hotel)
        {
            _context.Entry(hotel).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return hotel;
        }

        public async Task<Hotel> GetHotel(int id)
        {
            //  Hotel hotel = await _context.Hotels.FindAsync(id);
            //return hotel;
            Hotel hotel = await _context.Hotels.FindAsync(id);
            var hotelRooms = await _context.HotelRooms.Where(x => x.HotelId == id)
                                                      .Include(x => x.Room)
                                                      .ThenInclude(x => x.RoomAmenities)
                                                      .ThenInclude(x => x.Amenities)
                                                      .ToListAsync();
            hotel.HotelRooms = hotelRooms;
            return hotel;
        }

        public async Task<List<Hotel>> GetHotels()
        {
            var hotels = await _context.Hotels.Include(x => x.HotelRooms)
                                                    .ThenInclude(x => x.Room)
                                                    .ThenInclude(x => x.RoomAmenities)
                                                    .ThenInclude(x => x.Amenities)
                                                    .ToListAsync();
            return hotels;
        }

        public async Task<Hotel> UpdateHotel(int id, Hotel hotel)
        {
            _context.Entry(hotel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return hotel;
        }
        public async Task DeleteHotel(int id)
        {
            Hotel hotel = await GetHotel(id);
            _context.Entry(hotel).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
