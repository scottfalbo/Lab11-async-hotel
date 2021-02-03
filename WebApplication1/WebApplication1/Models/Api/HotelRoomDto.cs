using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncHotel.Models.Api
{
    public class HotelRoomDto
    {
        public int HotelId { get; set; }
        public int RoomNumber { get; set; }
        public decimal Rate { get; set; }
        public bool PetFriendly { get; set; }
        public int RoomId { get; set; }
        public RoomDto Room { get; set; }
    }

}
