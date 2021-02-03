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
    public class AmenitiesController : ControllerBase
    {
        private readonly IAmenities _amenity;

        public AmenitiesController(IAmenities amenity)
        {
            _amenity = amenity;
        }

        // GET: api/Amenities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AmenitiesDto>>> GetAmenities()
        {
            return Ok(await _amenity.GetAmenities());
        }

        // GET: api/Amenities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AmenitiesDto>> GetAmenity(int id)
        {
            var amenities = await _amenity.GetAmenity(id);

            if (amenities == null)
            {
                return NotFound();
            }

            return amenities;
        }

        // PUT: api/Amenities/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAmenities(int id, AmenitiesDto amenities)
        {
            if (id != amenities.Id)
            {
                return BadRequest();
            }

            var updatedAmenities = await _amenity.UpdateAmenities(id, amenities);
            return Ok(updatedAmenities);

        }

        // POST: api/Amenities
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Amenities>> PostAmenities(AmenitiesDto amenities)
        {
            await _amenity.Create(amenities);
            return CreatedAtAction("GetAmenities", new { id = amenities.Id }, amenities);
        }

        // DELETE: api/Amenities/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Amenities>> DeleteAmenities(int id)
        {
            await _amenity.DeleteAmenity(id);
            return NoContent();
        }

    }
}
