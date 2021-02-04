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


        [Fact]
        public async Task Can_Create_And_Save_A_New_Hotel_After_New_Mock_Hotel()
        {
            await CreateAndSaveTestHotel();
            var repository = new HotelRepository(_db);

            HotelDto newHotelDto = new HotelDto
            {
                Id = 5,
                Name = "Test Hotel 2",
                StreetAddress = "123 Test Street",
                City = "TestsVille",
                State = "Testington",
                Phone = "800-231-2344"
            };

            Hotel testHotel = await repository.Create(newHotelDto);

            var resultHotel = await repository.GetHotel(testHotel.Id);
            Assert.Equal("Test Hotel 2", resultHotel.Name);
        }

        [Fact]
        public async Task Can_Create_And_Save_A_New_Room_In_Mock_DB()
        {
            var testRoom = await CreateAndSaveTestRoom();
            var repository = new RoomRepository(_db);

            var resultRoom = await repository.GetRoom(testRoom.Id);

            Assert.Equal("Test Room", resultRoom.Name);
        }

        [Fact]
        public async Task Can_Create_And_Save_A_New_Amenity_In_Mock_DB()
        {
            var testAmenity = await CreateAndSaveTestAmenity();
            var repository = new AmenitiesRepository(_db);

            var resultAmenity = await repository.GetAmenity(testAmenity.Id);

            Assert.Equal("Mock Amenity", resultAmenity.AmenityName);
        }

        [Fact]
        public async Task Can_Delete_Hotel_From_The_Mock_Database()
        {
            var testHotel = CreateAndSaveTestHotel();
            var repository = new HotelRepository(_db);

            await repository.DeleteHotel(testHotel.Id);
            
            var result = await repository.GetHotel(4);
            Assert.Null(result);
        }


        [Fact]
        public async Task Can_Register_A_New_User_And_Add_Them_To_The_DB()
        {
            
        }
    }
}
