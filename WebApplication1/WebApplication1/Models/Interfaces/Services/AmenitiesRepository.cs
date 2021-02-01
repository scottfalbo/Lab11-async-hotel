using AsyncHotel.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncHotel.Models.Interfaces.Services
{
    public class AmenitiesRepository : IAmenities
    {
        private readonly AsyncDbContext _context;

        public AmenitiesRepository(AsyncDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Create a new ammenity in the DB
        /// </summary>
        /// <param name="amenities"> Amenties object </param>
        /// <returns> the new amenity object </returns>
        public async Task<Amenities> Create(Amenities amenities)
        {
            _context.Entry(amenities).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return amenities;
        }

        /// <summary>
        /// Get a single Amenity by ID
        /// </summary>
        /// <param name="id"> int Amenity ID </param>
        /// <returns> Amenities object </returns>
        public async Task<Amenities> GetAmenity(int id)
        {
            Amenities amenity = await _context.Amenities.Where(x => x.Id == id)
                                                        .Include(x => x.RoomAmenities)
                                                        .ThenInclude(x => x.Room)
                                                        .ThenInclude(x => x.HotelRooms)
                                                        .ThenInclude(x => x.Hotel)
                                                        .FirstOrDefaultAsync();
            return amenity;
        }

        /// <summary>
        /// Get a list of all stored Amenties
        /// </summary>
        /// <returns> List<Amenities> object </returns>
        public async Task<List<Amenities>> GetAmenities()
        {
            var amenities = await _context.Amenities.Include(x => x.RoomAmenities)
                                                    .ThenInclude(x => x.Room)
                                                    .ThenInclude(x => x.HotelRooms)
                                                    .ThenInclude(x => x.Hotel)
                                                    .ToListAsync();
            return amenities;
        }

        /// <summary>
        /// Edit and update an Amenties data
        /// </summary>
        /// <param name="id"> int Amenties ID </param>
        /// <param name="amenities"> Amenties object </param>
        /// <returns> updated Amenties object </returns>
        public async Task<Amenities> UpdateAmenities(int id, Amenities amenities)
        {
            _context.Entry(amenities).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return amenities;
        }

        /// <summary>
        /// Delete and Amenties object from the database
        /// </summary>
        /// <param name="id"> int amentiesId </param>
        /// <returns> no return </returns>
        public async Task DeleteAmenity(int id)
        {
            Amenities amenities = await GetAmenity(id);
            _context.Entry(amenities).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

    }
}
