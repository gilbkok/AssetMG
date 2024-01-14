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
    public class AssetOperationsController : ControllerBase
    {
        private readonly AssetMGDbContext _context;

        public AssetOperationsController(AssetMGDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAssetOperations()
        {
            try
            {
                var assetOperations = await _context.Operations
                    .Include(o => o.Assets)
                    .Include(o => o.Users)
                    .ToListAsync();

                return Ok(assetOperations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAssetOperationById(int id)
        {
            try
            {
                var assetOperation = await _context.Operations
                    .Include(o => o.Assets)
                    .Include(o => o.Users)
                    .FirstOrDefaultAsync(o => o.Operations_Id == id);

                if (assetOperation == null)
                {
                    return NotFound(); // HTTP 404 Not Found if the item is not found
                }

                return Ok(assetOperation); // HTTP 200 OK with the retrieved item
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAssetOperation([FromBody] Asset_Operations newAssetOperation)
        {
            try
            {
                _context.Operations.Add(newAssetOperation);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetAssetOperationById), new { id = newAssetOperation.Operations_Id }, newAssetOperation);
                // HTTP 201 Created with the newly created item and its location in the header
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAssetOperation(int id, [FromBody] Asset_Operations updatedAssetOperation)
        {
            try
            {
                if (id != updatedAssetOperation.Operations_Id)
                {
                    return BadRequest("ID mismatch"); // HTTP 400 Bad Request if ID doesn't match
                }

                _context.Entry(updatedAssetOperation).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent(); // HTTP 204 No Content on successful update
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssetOperation(int id)
        {
            try
            {
                var assetOperation = await _context.Operations.FindAsync(id);

                if (assetOperation == null)
                {
                    return NotFound(); // HTTP 404 Not Found if the item is not found
                }

                _context.Operations.Remove(assetOperation);
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
