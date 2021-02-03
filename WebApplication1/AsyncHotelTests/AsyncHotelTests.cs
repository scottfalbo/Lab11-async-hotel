using System;
using System.Collections.Generic;
using System.Text;
using AsyncHotel.Models;
using AsyncHotel.Data;
using Xunit;
using System.Threading.Tasks;
using AsyncHotel.Models.Interfaces.Services;

namespace AsyncHotelTests
{
    public class AsyncHotelTests : Mock
    {
        [Fact]
        protected async Task<Room> CreateAndSaveTestRoom()
        {
            var room = new Room { };
            _db.Rooms.Add(room);
            await _db.SaveChangesAsync();
            Assert.NotEqual(0, room.Id); // Sanity check
            return room;
        }

        [Fact]
        protected async Task<Hotel> CreateAndSaveTestHotel()
        {
            var hotel = new Hotel { };
            _db.Hotels.Add(hotel);
            await _db.SaveChangesAsync();
            Assert.NotEqual(0, hotel.Id); // Sanity check
            return hotel;
        }

        [Fact]
        public async Task Can_enroll_and_drop_a_student()
        {
            // Arrange
            var room = await CreateAndSaveTestRoom();
            var hotel = await CreateAndSaveTestHotel();

            //var repository = new HotelRepository(_db);

            // Act
            //await repository.AddRoom(c.Id, student.Id);

            // Assert
           // var actualCourse = await repository.GetOne(course.Id);

           // Assert.Contains(actualCourse.Enrollments, e => e.StudentId == student.Id);

            // Act
           // await repository.RemoveStudentFromCourse(course.Id, student.Id);

            // Assert
            //actualCourse = await repository.GetOne(course.Id);
           // Assert.DoesNotContain(actualCourse.Enrollments, e => e.StudentId == student.Id);
        }
    }
}
