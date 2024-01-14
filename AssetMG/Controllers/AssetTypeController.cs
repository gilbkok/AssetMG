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
    public class AssetTypeController : ControllerBase
    {
        private readonly AssetMGDbContext _context;

        public AssetTypeController(AssetMGDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAssetTypes()
        {
            try
            {
                var assetTypes = await _context.Asset_Type.ToListAsync();
                return Ok(assetTypes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAssetTypeById(int id)
        {
            try
            {
                var assetType = await _context.Asset_Type.FindAsync(id);

                if (assetType == null)
                {
                    return NotFound(); // HTTP 404 Not Found if the item is not found
                }

                return Ok(assetType); // HTTP 200 OK with the retrieved item
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAssetType([FromBody] Asset_Type newAssetType)
        {
            try
            {
                _context.Asset_Type.Add(newAssetType);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetAssetTypeById), new { id = newAssetType.ATId }, newAssetType);
                // HTTP 201 Created with the newly created item and its location in the header
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAssetType(int id, [FromBody] Asset_Type updatedAssetType)
        {
            try
            {
                if (id != updatedAssetType.ATId)
                {
                    return BadRequest("ID mismatch"); // HTTP 400 Bad Request if ID doesn't match
                }

                _context.Entry(updatedAssetType).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent(); // HTTP 204 No Content on successful update
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssetType(int id)
        {
            try
            {
                var assetType = await _context.Asset_Type.FindAsync(id);

                if (assetType == null)
                {
                    return NotFound(); // HTTP 404 Not Found if the item is not found
                }

                _context.Asset_Type.Remove(assetType);
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
