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
    public class AssetMovementController : ControllerBase
    {
        private readonly AssetMGDbContext _context;

        public AssetMovementController(AssetMGDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAssetMovements()
        {
            try
            {
                var assetMovements = await _context.Mvmt
                    .Include(m => m.Assets)
                    .Include(m => m.Type)
                    .Include(m => m.Users)
                    .ToListAsync();

                return Ok(assetMovements);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAssetMovementById(int id)
        {
            try
            {
                var assetMovement = await _context.Mvmt
                    .Include(m => m.Assets)
                    .Include(m => m.Type)
                    .Include(m => m.Users)
                    .FirstOrDefaultAsync(m => m.MId == id);

                if (assetMovement == null)
                {
                    return NotFound(); // HTTP 404 Not Found if the item is not found
                }

                return Ok(assetMovement); // HTTP 200 OK with the retrieved item
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAssetMovement([FromBody] Asset_Mvmt newAssetMovement)
        {
            try
            {
                _context.Mvmt.Add(newAssetMovement);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetAssetMovementById), new { id = newAssetMovement.MId }, newAssetMovement);
                // HTTP 201 Created with the newly created item and its location in the header
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAssetMovement(int id, [FromBody] Asset_Mvmt updatedAssetMovement)
        {
            try
            {
                if (id != updatedAssetMovement.MId)
                {
                    return BadRequest("ID mismatch"); // HTTP 400 Bad Request if ID doesn't match
                }

                _context.Entry(updatedAssetMovement).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent(); // HTTP 204 No Content on successful update
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssetMovement(int id)
        {
            try
            {
                var assetMovement = await _context.Mvmt.FindAsync(id);

                if (assetMovement == null)
                {
                    return NotFound(); // HTTP 404 Not Found if the item is not found
                }

                _context.Mvmt.Remove(assetMovement);
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
