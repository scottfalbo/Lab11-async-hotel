using AsyncHotel.Data;
using AsyncHotel.Models.Api;
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
        public async Task<HotelRoom> Create(HotelRoomDto inboudData, int hotelId)
        {
            HotelRoom hotelRoom = new HotelRoom()
            {
                RoomNumber = inboudData.RoomNumber,
                Rate = inboudData.Rate,
                PetFriendly = inboudData.PetFriendly
            };

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
        public async Task<HotelRoomDto> GetHotelRoom(int hotelId, int roomId)
        {
            return await _context.HotelRooms
                .Where(x => x.HotelId == hotelId && x.RoomId == roomId)
                .Select(hotelRoom => new HotelRoomDto
                {
                    RoomNumber = hotelRoom.RoomNumber,
                    Rate = hotelRoom.Rate,
                    PetFriendly = hotelRoom.PetFriendly
                }).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets a list of all HotelRoom objects for a Hotel in the DB
        /// </summary>
        /// <param name="hotelId"> int hotelId </param>
        /// <returns> HotelRoom objects for specifed Hotel object </returns>
        public async Task<List<HotelRoomDto>> GetHotelRooms(int hotelId)
        {
            return await _context.HotelRooms
                .Where(x => x.HotelId == hotelId)
                .Select(rooms => new HotelRoomDto
                {
                    RoomNumber = rooms.RoomNumber,
                    Rate = rooms.Rate,
                    PetFriendly = rooms.PetFriendly
                }).ToListAsync();
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
            HotelRoomDto hotelRoom = await GetHotelRoom(hotelId, roomId);
            _context.Entry(hotelRoom).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

    }
}
