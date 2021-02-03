using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncHotel.Models
{
    public class Amenities
    {
        public int Id { get; set; }
        [Required]
        public string AmenityName { get; set; }
        [Required]
        public List<RoomAmenities> RoomAmenities { get; set; }
    }
}
