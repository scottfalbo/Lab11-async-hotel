using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AsyncHotel.Data;
using AsyncHotel.Models;
using AsyncHotel.Models.Interfaces;
using AsyncHotel.Models.Api;

namespace AsyncHotel.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelRoomsController : ControllerBase
    {
        private readonly IHotelRoom _hotelRoom;

        public HotelRoomsController(IHotelRoom hotelRoom)
        {
            _hotelRoom = hotelRoom;
        }

        // POST: api/HotelRooms/
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<HotelRoom>> PostHotelRoom(HotelRoom hotelRoom, int hotelId)
        {
            await _hotelRoom.Create(hotelRoom, hotelId);

            return CreatedAtAction("GetHotelRoom", new { id = hotelRoom.HotelId }, hotelRoom);
        }

        // GET: api/HotelRooms/n
        [HttpGet]
        [Route("{hotelId}")]
        //[Route]
        public async Task<ActionResult<IEnumerable<HotelRoomDto>>> GetHotelRooms(int hotelId)
        {
            return await _hotelRoom.GetHotelRooms(hotelId);
        }

        // GET: api/HotelRooms/n/n
        [HttpGet]
        [Route("{hotelId}/{roomId}")]
        public async Task<ActionResult<HotelRoomDto>> GetHotelRoom(int hotelId, int roomId)
        {
            return await _hotelRoom.GetHotelRoom(hotelId, roomId);
        }

        // PUT: api/HotelRooms/n/n
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        [Route("{hotelId}/{roomId}")]
        public async Task<IActionResult> PutHotelRoom(HotelRoom hotelRoom, int hotelId, int roomId)
        {
            if (hotelId != hotelRoom.HotelId || roomId != hotelRoom.RoomId)
                return BadRequest();

            var updatedHotelRoom = await _hotelRoom.UpdateHotelRoom(hotelRoom);

            return Ok(updatedHotelRoom);
        }

        // DELETE: api/HotelRooms/n/n
        [HttpDelete]
        [Route("{hotelId}/{roomId}")]
        public async Task<ActionResult<HotelRoom>> DeleteHotelRoom(int hotelId, int roomId)
        {
            await _hotelRoom.DeleteHotelRoom(hotelId, roomId);
            return NoContent();
        }
    }
}
