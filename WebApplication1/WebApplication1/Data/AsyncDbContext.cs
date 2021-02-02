using AsyncHotel.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncHotel.Data
{
    public class AsyncDbContext : DbContext
    {
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<AmenitiesDto> Amenities { get; set; }
        public DbSet<HotelRoom> HotelRooms { get; set; }
        public DbSet<RoomAmenities> RoomAmenities { get; set; }

        public AsyncDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // This calls the base method, but does nothing
            // base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<HotelRoom>().HasKey(
                x => new { x.HotelId, x.RoomId });

            modelBuilder.Entity<RoomAmenities>().HasKey(
                x => new { x.RoomId, x.AmenitiesId });

            modelBuilder.Entity<Hotel>().HasData(
              new Hotel 
              { 
                  Id = 1,
                  Name = "The Overlook",
                  StreetAddress = "333 Wonderview Avenue",
                  City = "Estes Park",
                  State = "Colorado",
                  Country = "United States",
                  Phone = "833-888-0237"
              },
              new Hotel
              {
                  Id = 2,
                  Name = "Motel Murder",
                  StreetAddress = "Route 66",
                  City = "Death Valley",
                  State = "Nevada",
                  Country = "United States",
                  Phone = "702-654-3693"
              },
              new Hotel
              {
                  Id = 3,
                  Name = "Fake Hotel",
                  StreetAddress = "123 Some Street",
                  City = "Townsville",
                  State = "Stateton",
                  Country = "Somewheres",
                  Phone = "111-313-56743"
              }
              );
            
            modelBuilder.Entity<Room>().HasData(
                new Room
                {
                    Id = 1,
                    RoomName = "237",
                    Layout = Layouts.OneBedroom
                },
                new Room
                {
                    Id = 2,
                    RoomName = "Bates Room",
                    Layout = Layouts.Studio
                },
                new Room
                {
                    Id = 3,
                    RoomName = "Grand Ole Room",
                    Layout = Layouts.TwoBedroom
                }
                );

            modelBuilder.Entity<AmenitiesDto>().HasData(
                new AmenitiesDto
                {
                    Id = 1,
                    AmenityName = "Creepy old lady"
                },
                 new AmenitiesDto
                 {
                     Id = 2,
                     AmenityName = "Stabbiness"
                 },
                new AmenitiesDto
                {
                    Id = 3,
                    AmenityName = "Mini bar"
                }
                );
            modelBuilder.Entity<RoomAmenities>().HasData(
                new RoomAmenities
                {
                    RoomId = 1,
                    AmenitiesId = 1
                },
                new RoomAmenities
                {
                    RoomId = 2,
                    AmenitiesId = 2
                },
                new RoomAmenities
                {
                    RoomId = 3,
                    AmenitiesId = 3
                });
            modelBuilder.Entity<HotelRoom>().HasData(
                new HotelRoom
                {
                    HotelId = 1,
                    RoomId = 1
                },
                new HotelRoom
                {
                    HotelId = 2,
                    RoomId = 2
                },
                new HotelRoom
                {
                    HotelId = 3,
                    RoomId = 3
                });

        }

    }
}
