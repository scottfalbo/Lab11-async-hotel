﻿using AsyncHotel.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncHotel.Models.Interfaces
{
    public interface IHotelRoom
    {
        Task<HotelRoom> Create(HotelRoomDto hotelRoom, int hotelId);
        Task<HotelRoomDto> GetHotelRoom(int hotelId, int roomId);
        Task<List<HotelRoomDto>> GetHotelRooms(int hotelId);
        Task<HotelRoom> UpdateHotelRoom(HotelRoomDto hotelRoom);
        Task DeleteHotelRoom(int hotelId, int roomId);
    }
}
