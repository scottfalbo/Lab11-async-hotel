using AsyncHotel.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncHotel.Models.Interfaces
{
    public interface IAmenities
    {
        Task<Amenities> Create(Amenities amenities);
        Task<AmenitiesDto> GetAmenity(int id);
        Task<List<AmenitiesDto>> GetAmenities();
        Task<AmenitiesDto> UpdateAmenities(int id, AmenitiesDto amenities);
        Task DeleteAmenity(int id);
    }
}
