using AsyncHotel.Models.Api;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncHotel.Models
{
    public class RoomAmenities
    {
        public int RoomId { get; set; }
        public int AmenitiesId { get; set; }

        public AmenitiesDto Amenities { get; set; }
        public Room Room { get; set; }
    }
}
