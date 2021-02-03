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
        public async Task<Hotel> Create(Hotel inboudData)
        {
            Hotel hotel = new Hotel()
            {
                Id =inboudData.Id,
                Name = inboudData.Name,
                StreetAddress = inboudData.StreetAddress,
                City = inboudData.City,
                State = inboudData.State,
                Phone = inboudData.Phone,
            };
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
            HotelDto hotel = await _context.Hotels
                .Where(x => x.Id == id)
                .Select(x => new HotelDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    StreetAddress = x.StreetAddress,
                    City = x.City,
                    State = x.State,
                    Phone = x.Phone,
                    Rooms = x.HotelRooms
                    .Select(y => new HotelRoomDto()
                    {
                        HotelId = y.HotelId,
                        RoomNumber = y.RoomNumber,
                        Rate = y.Rate,
                        PetFriendly = y.PetFriendly,
                        RoomId = y.RoomId,
                        Room = y.Room.HotelRooms
                        .Select(z => new RoomDto()
                    {
                        Id = z.Room.Id,
                        Name = z.Room.RoomName,
                        Layout = z.Room.Layout,
                    })
                    .FirstOrDefault()
                })
                .ToList()
                })
                .FirstOrDefaultAsync();

            foreach (var hotelRoom in hotel.Rooms)
            {
                var roomAmenity = await _context.RoomAmenities
                .Where(x => x.RoomId == hotelRoom.RoomId)
                .Select(y => new AmenitiesDto()
                {
                    Id = y.Amenities.Id,
                    AmenityName = y.Amenities.AmenityName
                })
                .ToListAsync();


            hotelRoom.Room.Amenities = roomAmenity;
            }
            return hotel;
        }

        /// <summary>
        /// Get a list of all of the Hotel objects in the DB
        /// </summary>
        /// <returns> List<Hotels> all hotels </returns>
        public async Task<List<HotelDto>> GetHotels()
        {
            var hotels = await _context.Hotels
                .Select(x => new HotelDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    StreetAddress = x.StreetAddress,
                    City = x.City,
                    State = x.State,
                    Phone = x.Phone,
                    Rooms = x.HotelRooms
                    .Select(y => new HotelRoomDto()
                    {
                        HotelId = y.HotelId,
                        RoomNumber = y.RoomNumber,
                        Rate = y.Rate,
                        PetFriendly = y.PetFriendly,
                        RoomId = y.RoomId,
                        Room = y.Room.HotelRooms
                        .Select(z => new RoomDto()
                        {
                            Id = z.Room.Id,
                            Name = z.Room.RoomName,
                            Layout = z.Room.Layout,
                        })
                        .FirstOrDefault()
                    })
                    .ToList()
                })
                .ToListAsync();

            foreach (var hotel in hotels)
            {
                foreach (var rooms in hotel.Rooms)
                {
                    var amenity = await _context.RoomAmenities
                    .Where(x => x.RoomId == rooms.RoomId)
                    .Select(y => new AmenitiesDto()
                    {
                        Id = y.Amenities.Id,
                        AmenityName = y.Amenities.AmenityName
                    })
                    .ToListAsync();
                    rooms.Room.Amenities = amenity;
                }
            }

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
            Hotel updatedHotel = new Hotel()
            {
                Id = hotel.Id,
                Name = hotel.Name,
                StreetAddress = hotel.StreetAddress,
                City = hotel.City,
                State = hotel.State,
                Phone = hotel.Phone
            };
            _context.Entry(updatedHotel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return updatedHotel;
        }

        /// <summary>
        /// Delete a Hotel object from the DB
        /// </summary>
        /// <param name="id"> int hotelId </param>
        /// <returns> no return </returns>
        public async Task DeleteHotel(int id)
        {
            Hotel hotel = await _context.Hotels.FindAsync(id);
            _context.Entry(hotel).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
