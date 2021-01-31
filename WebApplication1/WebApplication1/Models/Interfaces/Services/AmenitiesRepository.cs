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

        public async Task<Amenities> Create(Amenities amenities)
        {
            _context.Entry(amenities).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return amenities;
        }

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

        public async Task<List<Amenities>> GetAmenities()
        {
            var amenities = await _context.Amenities.Include(x => x.RoomAmenities)
                                                    .ThenInclude(x => x.Room)
                                                    .ThenInclude(x => x.HotelRooms)
                                                    .ThenInclude(x => x.Hotel)
                                                    .ToListAsync();
            return amenities;
        }

        public async Task<Amenities> UpdateAmenities(int id, Amenities amenities)
        {
            _context.Entry(amenities).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return amenities;
        }
        public async Task DeleteAmenity(int id)
        {
            Amenities amenities = await GetAmenity(id);
            _context.Entry(amenities).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

    }
}
