using AsyncHotel.Models;
using AsyncHotel.Models.Api;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncHotel.Data
{
    public class AsyncDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Amenities> Amenities { get; set; }
        public DbSet<HotelRoom> HotelRooms { get; set; }
        public DbSet<RoomAmenities> RoomAmenities { get; set; }

        public AsyncDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // We need this for Identity to do it's work before we do next..
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<HotelRoom>().HasKey(
                x => new { x.HotelId, x.RoomNumber });

            modelBuilder.Entity<RoomAmenities>().HasKey(
                x => new { x.RoomId, x.AmenitiesId });

            //TODO: todays stuff, create roles
            SeedRole(modelBuilder, "District Manager", "create", "update", "delete");
            SeedRole(modelBuilder, "Property Manager", "create", "update", "delete");
            SeedRole(modelBuilder, "Agent", "create", "update", "delete");

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

            modelBuilder.Entity<Amenities>().HasData(
                new Amenities
                {
                    Id = 1,
                    AmenityName = "Creepy old lady"
                },
                 new Amenities
                 {
                     Id = 2,
                     AmenityName = "Stabbiness"
                 },
                new Amenities
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
                    RoomNumber = 1,
                    RoomId = 1
                },
                new HotelRoom
                {
                    HotelId = 2,
                    RoomNumber = 2,
                    RoomId = 2
                },
                new HotelRoom
                {
                    HotelId = 3,
                    RoomNumber = 3,
                    RoomId = 3
                });
        }


        private int id = 1;
        private void SeedRole (ModelBuilder modelBuilder, string roleName, params string[] permissions)
        {
            var role = new IdentityRole
            {
                Id = roleName.ToLower(),
                Name = roleName,
                NormalizedName = roleName.ToUpper(),
                ConcurrencyStamp = Guid.Empty.ToString()
            };
            modelBuilder.Entity<IdentityRole>().HasData(role);

            var roleClaims = permissions.Select(permission =>
               new IdentityRoleClaim<string>
               {
                    Id = id++,
                    RoleId = role.Id,
                    ClaimType = "permissions",
                    ClaimValue = permission
               });
            modelBuilder.Entity<IdentityRoleClaim<string>>().HasData(roleClaims);
            //claim type comes from startup seed method call, line 90ish
        }

    }
}
