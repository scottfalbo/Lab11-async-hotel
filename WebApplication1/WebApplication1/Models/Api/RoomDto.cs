using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncHotel.Models.Api
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Layout { get; set; }
        public List<AmenitiesDto> Amenities { get; set; }
    }
}
