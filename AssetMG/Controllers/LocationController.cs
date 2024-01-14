using AssetMG.Data;
using AssetMG.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AssetMG.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AssetLocationController : ControllerBase
    {
        private readonly AssetMGDbContext _context;

        public AssetLocationController(AssetMGDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLocations()
        {
            try
            {
                var locations = await _context.Locations.ToListAsync();
                return Ok(locations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLocationById(int id)
        {
            try
            {
                var location = await _context.Locations.FindAsync(id);

                if (location == null)
                {
                    return NotFound(); // HTTP 404 Not Found if the item is not found
                }

                return Ok(location); // HTTP 200 OK with the retrieved item
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateLocation([FromBody] Asset_Location newLocation)
        {
            try
            {
                _context.Locations.Add(newLocation);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetLocationById), new { id = newLocation.ALId }, newLocation);
                // HTTP 201 Created with the newly created item and its location in the header
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLocation(int id, [FromBody] Asset_Location updatedLocation)
        {
            try
            {
                if (id != updatedLocation.ALId)
                {
                    return BadRequest("ID mismatch"); // HTTP 400 Bad Request if ID doesn't match
                }

                _context.Entry(updatedLocation).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent(); // HTTP 204 No Content on successful update
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            try
            {
                var location = await _context.Locations.FindAsync(id);

                if (location == null)
                {
                    return NotFound(); // HTTP 404 Not Found if the item is not found
                }

                _context.Locations.Remove(location);
                await _context.SaveChangesAsync();

                return NoContent(); // HTTP 204 No Content on successful deletion
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}