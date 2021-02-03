using AsyncHotel.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AsyncHotel.Models.Api;

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
        public async Task<Amenities> Create(Amenities inboundData)
        {
            Amenities amenity = new Amenities()
            {
                AmenityName = inboundData.AmenityName
            };
            _context.Entry(amenity).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return amenity;
        }

        /// <summary>
        /// Get a single Amenity by ID
        /// </summary>
        /// <param name="id"> int Amenity ID </param>
        /// <returns> Amenities object </returns>
        public async Task<AmenitiesDto> GetAmenity(int id)
        {
            return await _context.Amenities
                .Where(x => x.Id == id)
                .Select(amenity => new AmenitiesDto
                {
                    Id = amenity.Id,
                    AmenityName = amenity.AmenityName
                }).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Get a list of all stored Amenties
        /// </summary>
        /// <returns> List<Amenities> object </returns>
        public async Task<List<AmenitiesDto>> GetAmenities()
        {
            return await _context.Amenities
                .Select(amenity => new AmenitiesDto
                {
                    Id = amenity.Id,
                    AmenityName = amenity.AmenityName
                }).ToListAsync();
        }

        /// <summary>
        /// Edit and update an Amenties data
        /// </summary>
        /// <param name="id"> int Amenties ID </param>
        /// <param name="amenities"> Amenties object </param>
        /// <returns> updated Amenties object </returns>
        public async Task<AmenitiesDto> UpdateAmenities(int id, AmenitiesDto amenities)
        {
            Amenities updatedAmenity = new Amenities()
            {
                Id = id,
                AmenityName = amenities.AmenityName
            };

            _context.Entry(updatedAmenity).State = EntityState.Modified;
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
            Amenities amenity = await _context.Amenities.FindAsync(id);
            _context.Entry(amenity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

    }
}
