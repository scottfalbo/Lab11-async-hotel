using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncHotel.Models.Interfaces
{
    public interface IAmenities
    {
        Task<Amenities> Create(Amenities amenities);
        Task<Amenities> GetAmenity(int id);
        Task<List<Amenities>> GetAmenities();
        Task<Amenities> UpdateAmenities(int id, Amenities amenities);
        Task DeleteAmenity(int id);
    }
}
