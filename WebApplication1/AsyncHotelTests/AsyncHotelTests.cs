using System;
using System.Collections.Generic;
using System.Text;
using AsyncHotel.Models;
using AsyncHotel.Data;
using Xunit;
using System.Threading.Tasks;

namespace AsyncHotelTests
{
    public class AsyncHotelTests : Mock
    {
        [Fact]
        protected async Task<Hotel> CreateAndSaveTestStudent()
        {
            var hotel = new Hotel { };
            _db.Hotels.Add(hotel);
            await _db.SaveChangesAsync();
            Assert.NotEqual(0, hotel.Id); // Sanity check
            return hotel;
        }

        [Fact]
        protected async Task<Hotel> CreateAndSaveTestCourse()
        {
            var hotel = new Hotel { };
            _db.Hotels.Add(hotel);
            await _db.SaveChangesAsync();
            Assert.NotEqual(0, hotel.Id); // Sanity check
            return hotel;
        }
    }
}
