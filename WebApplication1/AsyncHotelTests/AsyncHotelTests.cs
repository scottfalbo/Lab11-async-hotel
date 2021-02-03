using System;
using System.Collections.Generic;
using System.Text;
using AsyncHotel.Models;
using AsyncHotel.Data;
using Xunit;
using System.Threading.Tasks;
using AsyncHotel.Models.Interfaces.Services;
using AsyncHotel.Models.Api;

namespace AsyncHotelTests
{
    public class AsyncHotelTests : Mock
    {
        //[Fact]
        protected async Task<Room> CreateAndSaveTestRoom()
        {
            var room = new Room
            {
                Id = 1,
                RoomName = "Test Room", 
                Layout = Layouts.TwoBedroom 
            };
            _db.Rooms.Add(room);
            await _db.SaveChangesAsync();
            //Assert.NotEqual(0, room.Id); // Sanity check
            return room;
        }

        //[Fact]
        protected async Task<Hotel> CreateAndSaveTestHotel()
        {
            var hotel = new Hotel
            {
                Id = 1,
                Name = "Test Hotel",
                StreetAddress = "123 Somewhere",
                City = "TestVilee",
                State = "StatesVille",
                Phone = "816-342-5462"
            };
            _db.Hotels.Add(hotel);
            await _db.SaveChangesAsync();
            //Assert.NotEqual(0, hotel.Id); // Sanity check
            return hotel;
        }

        //[Fact]
        public async Task Can_Do_Something()
        {
            var room = new Room()
            {
                Id = 1,
                RoomName = "Cat Shack",
                Layout = Layouts.Studio
            };

            var repository = new RoomRepository(_db);

            var testRoom = await repository.Create(room);

            //Assert.Equal(room.Id, testRoom.Id);
        }
    }
}
