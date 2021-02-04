using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using AsyncHotel.Models;
using AsyncHotel.Data;
using AsyncHotel.Models.Api;
using System.Threading.Tasks;
using Xunit;

namespace AsyncHotelTests
{
    public abstract class Mock : IDisposable
    {
        private readonly SqliteConnection _connection;
        protected readonly AsyncDbContext _db;

        /// <summary>
        /// Constructor to create a mock database based on the AsyncDbContext
        /// </summary>
        public Mock()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            _db = new AsyncDbContext(
                new DbContextOptionsBuilder<AsyncDbContext>()
                    .UseSqlite(_connection)
                    .Options);

            _db.Database.EnsureCreated();
        }

        /// <summary>
        /// Self destruct method for when the test is done with the mock database
        /// </summary>
        public void Dispose()
        {
            _db?.Dispose();
            _connection?.Dispose();
        }

        protected async Task<Amenities> CreateAndSaveTestAmenity()
        {
            var amenity = new Amenities
            {
                Id = 4,
                AmenityName = "Mock Amenity"
            };
            _db.Amenities.Add(amenity);
            await _db.SaveChangesAsync();
            _db.Entry(amenity).State = EntityState.Detached;
            Assert.NotEqual(0, amenity.Id); // Sanity check
            return amenity;
        }

        /// <summary>
        /// Create Room object and save it to the mock database
        /// </summary>
        /// <returns> the new Room object </returns>
        protected async Task<Room> CreateAndSaveTestRoom()
        {
            var room = new Room
            {
                Id = 4,
                RoomName = "Test Room",
                Layout = Layouts.TwoBedroom
            };
            _db.Rooms.Add(room);
            await _db.SaveChangesAsync();
            _db.Entry(room).State = EntityState.Detached;
            Assert.NotEqual(0, room.Id); // Sanity check
            return room;
        }

        /// <summary>
        /// Create a Hotel object and save it to the mock database
        /// </summary>
        /// <returns> the new Hotel object </returns>
        protected async Task<Hotel> CreateAndSaveTestHotel()
        {
            var hotel = new Hotel
            {
                Id = 4,
                Name = "Test Hotel",
                StreetAddress = "123 Test Street",
                City = "TestsVille",
                State = "Testington",
                Phone = "800-231-2344"
            };
            _db.Hotels.Add(hotel);
            await _db.SaveChangesAsync();
            _db.Entry(hotel).State = EntityState.Detached;
            Assert.NotEqual(0, hotel.Id); // Sanity check
            return hotel;
        }
    }
}
