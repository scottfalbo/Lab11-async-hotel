﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AsyncHotel.Data;
using AsyncHotel.Models.Api;
using Microsoft.EntityFrameworkCore;

namespace AsyncHotel.Models.Interfaces.Services
{
    public class RoomRepository : IRoom
    {
        private readonly AsyncDbContext _context;

        public RoomRepository(AsyncDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Create and add a new room to the database
        /// </summary>
        /// <param name="room"> Room object </param>
        /// <returns> the same object </returns>
        public async Task<Room> Create(RoomDto inboundData)
        {
            // Converts the RoomDto's input string to enum value           
            Enum.TryParse(inboundData.Layout, out Layouts layout);

            Room room = new Room()
            {
                RoomName = inboundData.Name,
                Layout = layout
            };
            _context.Entry(room).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return room;
        }

        /// <summary>
        /// Gets a Room and amenities by RoomID from the DB
        /// </summary>
        /// <param name="id"> int RoomId </param>
        /// <returns> Room object from DB </returns>
        public async Task<RoomDto> GetRoom(int id)
        {
            return await _context.Rooms
                .Where(x => x.Id == id)
                .Select(room => new RoomDto
                {
                    Id = room.Id,
                    Name = room.RoomName,
                    Layout = room.Layout.ToString(),
                    Amenities = room.RoomAmenities
                                .Select(amenities => new AmenitiesDto
                                {
                                    Id = amenities.Amenities.Id,
                                    AmenityName = amenities.Amenities.AmenityName
                                }).ToList()

                }).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets a List of all of the rooms and amenities from the DB
        /// </summary>
        /// <returns> a List of all of the rooms </returns>
        public async Task<List<RoomDto>> GetRooms()
        {
            return await _context.Rooms
                .Select(room => new RoomDto
                {
                    Id = room.Id,
                    Name = room.RoomName,
                    Layout = room.Layout.ToString(),
                    Amenities = room.RoomAmenities
                                .Select(amenities => new AmenitiesDto
                                {
                                    Id = amenities.Amenities.Id,
                                    AmenityName = amenities.Amenities.AmenityName
                                }).ToList()
                }).ToListAsync();
        }

        /// <summary>
        /// Update a room properties
        /// </summary>
        /// <param name="id"> int roomId </param>
        /// <param name="room"> Room object </param>
        /// <returns> updated Room object </returns>
        public async Task<Room> UpdateRoom(int id, Room room)
        {
            _context.Entry(room).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return room;
        }

        /// <summary>
        /// Add an amenity to a room by amenityID and room ID
        /// </summary>
        /// <param name="roomId"> int roomID </param>
        /// <param name="amenitiesId"> int amenitiesId </param>
        /// <returns> no return </returns>
        public async Task AddAmenityToRoom(int roomId, int amenitiesId)
        {
            RoomAmenities roomAmenity = new RoomAmenities()
            {
                RoomId = roomId,
                AmenitiesId = amenitiesId
            };
            _context.Entry(roomAmenity).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete a Room object from the DB by ID
        /// </summary>
        /// <param name="id"> int roomId </param>
        /// <returns> no return </returns>
        public async Task DeleteRoom(int id)
        {
            RoomDto room = await GetRoom(id);
            _context.Entry(room).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Remove an amenity from a room
        /// </summary>
        /// <param name="roomId"> int roomId </param>
        /// <param name="amenitiesId"> int amenitiesId </param>
        /// <returns> no return </returns>
        public async Task RemoveAmenityFromRoom(int roomId, int amenitiesId)
        {
            var result = await _context.RoomAmenities.FirstOrDefaultAsync(
                x => x.RoomId == roomId && x.AmenitiesId == amenitiesId);

            _context.Entry(result).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }
    }
}
