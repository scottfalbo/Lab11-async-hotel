using AsyncHotel.Data;
using AsyncHotel.Models.Api;
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

        /// <summary>
        /// Create a new Hotel object and save it to DB
        /// </summary>
        /// <param name="hotel"> new Hotel object </param>
        /// <returns> newly added Hotel object </returns>
        public async Task<Hotel> Create(Hotel hotel)
        {
            _context.Entry(hotel).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return hotel;
        }

        /// <summary>
        /// Get a single Hotel object by Id
        /// </summary>
        /// <param name="id"> int hotelId </param>
        /// <returns> Hotel object </returns>
        public async Task<HotelDto> GetHotel(int id)
        {
            HotelDto hotel = await _context.Hotels.FindAsync(id);
            var hotelRooms = await _context.HotelRooms.Where(x => x.HotelId == id)
                                                      .Include(x => x.Room)
                                                      .ThenInclude(x => x.RoomAmenities)
                                                      .ThenInclude(x => x.Amenities)
                                                      .ToListAsync();
            hotel.HotelRooms = hotelRooms;
            return hotel;
        }

        /// <summary>
        /// Get a list of all of the Hotel objects in the DB
        /// </summary>
        /// <returns> List<Hotels> all hotels </returns>
        public async Task<List<Hotel>> GetHotels()
        {
            var hotels = await _context.Hotels.Include(x => x.HotelRooms)
                                                    .ThenInclude(x => x.Room)
                                                    .ThenInclude(x => x.RoomAmenities)
                                                    .ThenInclude(x => x.Amenities)
                                                    .ToListAsync();
            return hotels;
        }

        /// <summary>
        /// Update a hotels data in the DB
        /// </summary>
        /// <param name="id"> int hotelId </param>
        /// <param name="hotel"> Hotel object </param>
        /// <returns> updated Hotel object </returns>
        public async Task<Hotel> UpdateHotel(int id, Hotel hotel)
        {
            _context.Entry(hotel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return hotel;
        }

        /// <summary>
        /// Delete a Hotel object from the DB
        /// </summary>
        /// <param name="id"> int hotelId </param>
        /// <returns> no return </returns>
        public async Task DeleteHotel(int id)
        {
            HotelDto hotel = await GetHotel(id);
            _context.Entry(hotel).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
