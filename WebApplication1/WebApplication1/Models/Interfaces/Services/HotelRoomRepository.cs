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

        /// <summary>
        /// Create a new HotelRoom object
        /// </summary>
        /// <param name="hotelRoom"> HotelRoom object </param>
        /// <param name="hotelId"> int hotelId </param>
        /// <returns> new HotelRoom object </returns>
        public async Task<HotelRoom> Create(HotelRoom hotelRoom, int hotelId)
        {
            _context.Entry(hotelRoom).State = EntityState.Added;
            await _context.SaveChangesAsync();

            return hotelRoom;
        }

        /// <summary>
        /// Get HotelRoom by composite ID of Hotel and Room objects.
        /// </summary>
        /// <param name="hotelId"> int hotelId </param>
        /// <param name="roomId"> int roomId </param>
        /// <returns> HotelRoom object </returns>
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

        /// <summary>
        /// Gets a list of all HotelRoom objects for a Hotel in the DB
        /// </summary>
        /// <param name="hotelId"> int hotelId </param>
        /// <returns> HotelRoom objects for specifed Hotel object </returns>
        public async Task<List<HotelRoom>> GetHotelRooms(int hotelId)
        {
            List<HotelRoom> hotelRooms = await _context.HotelRooms.Where(x => x.HotelId == hotelId)
                                                                  .Include(x => x.Room)
                                                                  .ThenInclude(x => x.RoomAmenities)
                                                                  .ThenInclude(x => x.Amenities)
                                                                  .ToListAsync();
            return hotelRooms;
        }

        /// <summary>
        /// Update a HotelRoom objects data in the DB
        /// </summary>
        /// <param name="hotelRoom"> HotelRoom object </param>
        /// <returns> updated HotelRoom object </returns>
        public async Task<HotelRoom> UpdateHotelRoom(HotelRoom hotelRoom)
        {
            _context.Entry(hotelRoom).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return hotelRoom;
        }

        /// <summary>
        /// Delete a HotelRoom object from the DB
        /// </summary>
        /// <param name="hotelId"> int hotelId </param>
        /// <param name="roomId"> int RoomId </param>
        /// <returns> no return </returns>
        public async Task DeleteHotelRoom(int hotelId, int roomId)
        {
            HotelRoom hotelRoom = await GetHotelRoom(hotelId, roomId);
            _context.Entry(hotelRoom).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

    }
}
