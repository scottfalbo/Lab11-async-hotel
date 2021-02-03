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
        public async Task<HotelRoom> Create(HotelRoom inboudData, int hotelId)
        {
            HotelRoom hotelRoom = new HotelRoom()
            {
                HotelId = inboudData.HotelId,
                RoomId = inboudData.RoomId,
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
        public async Task<HotelRoomDto> GetHotelRoom(int hotelId, int roomNumber)
        {
            HotelRoom hotelRoom = await _context.HotelRooms
                 .Where(x => x.HotelId == hotelId && x.RoomNumber == roomNumber)
                 .Include(x => x.Hotel)
                 .Include(x => x.Room)
                 .ThenInclude(x => x.RoomAmenities)
                 .ThenInclude(x => x.Amenities)
                 .FirstOrDefaultAsync();

            
            HotelRoomDto hotelRoomDTO = new HotelRoomDto()
            {
                HotelId = hotelRoom.HotelId,
                RoomNumber = hotelRoom.RoomNumber,
                Rate = hotelRoom.Rate,
                PetFriendly = hotelRoom.PetFriendly,
                RoomId = hotelRoom.RoomId,
                Room = new RoomDto()
                {
                    Id = hotelRoom.Room.Id,
                    Name = hotelRoom.Room.RoomName,
                    Layout = hotelRoom.Room.Layout,
                    Amenities = hotelRoom.Room.RoomAmenities
                        .Select(x => new AmenitiesDto()
                         {
                             Id = x.Amenities.Id,
                             AmenityName = x.Amenities.AmenityName
                         })
                         .ToList()
                }
            };
            return hotelRoomDTO;

        }

        /// <summary>
        /// Gets a list of all HotelRoom objects for a Hotel in the DB
        /// </summary>
        /// <param name="hotelId"> int hotelId </param>
        /// <returns> HotelRoom objects for specifed Hotel object </returns>
        public async Task<List<HotelRoomDto>> GetHotelRooms(int hotelId)
        {
            var hotelRoom = await _context.HotelRooms
                .Select(x => new HotelRoomDto()
                {
                     HotelId = x.HotelId,
                     RoomNumber = x.RoomNumber,
                     Rate = x.Rate,
                     PetFriendly = x.PetFriendly,
                     RoomId = x.RoomId,
                     Room = new RoomDto()
                     {
                         Id = x.Room.Id,
                         Name = x.Room.RoomName,
                         Layout = x.Room.Layout,
                         Amenities = x.Room.RoomAmenities
                                .Select(x => new AmenitiesDto()
                                {
                                    Id = x.Amenities.Id,
                                    AmenityName = x.Amenities.AmenityName        
                                })
                                .ToList()
                     }
             })
             .ToListAsync();

            return hotelRoom;
        }

        /// <summary>
        /// Update a HotelRoom objects data in the DB
        /// </summary>
        /// <param name="hotelRoom"> HotelRoom object </param>
        /// <returns> updated HotelRoom object </returns>
        public async Task<HotelRoom> UpdateHotelRoom(HotelRoom hotelRoom)
        {
            HotelRoom updatedHotelRoom = new HotelRoom()
            {
                HotelId = hotelRoom.HotelId,
                RoomId = hotelRoom.RoomId,
                RoomNumber = hotelRoom.RoomNumber,
                Rate = hotelRoom.Rate,
                PetFriendly = hotelRoom.PetFriendly
            };

            _context.Entry(hotelRoom).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return updatedHotelRoom;
        }

        /// <summary>
        /// Delete a HotelRoom object from the DB
        /// </summary>
        /// <param name="hotelId"> int hotelId </param>
        /// <param name="roomId"> int RoomId </param>
        /// <returns> no return </returns>
        public async Task DeleteHotelRoom(int hotelId, int roomNumber)
        {
            HotelRoom hotelRoom = await _context.HotelRooms
                .Where(x => x.HotelId == hotelId && x.RoomNumber == roomNumber)
                .FirstOrDefaultAsync();
            _context.Entry(hotelRoom).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

    }
}
