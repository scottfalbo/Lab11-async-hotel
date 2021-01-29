using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncHotel.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string RoomName { get; set; }
        [Required]
        public Layouts Layout { get; set; }
        
        public List<HotelRoom> HotelRooms { get; set; }
        public List <RoomAmenities> RoomAmenities { get; set; }

    }

    public enum Layouts
    {
        Studio,
        OneBedroom,
        TwoBedroom
    } 

}
