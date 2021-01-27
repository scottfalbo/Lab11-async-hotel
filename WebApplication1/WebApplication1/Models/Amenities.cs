using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncHotel.Models
{
    public class Amenities
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string AmenityName { get; set; }

    }
}
