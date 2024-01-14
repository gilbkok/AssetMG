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
    public class AssetsController : ControllerBase
    {
        private readonly AssetMGDbContext _context;

        public AssetsController(AssetMGDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAssets()
        {
            try
            {
                var assets = await _context.Assets
                    .Include(a => a.AssetType)
                    .Include(a => a.CreatedByUser)
                    .Include(a => a.Department)
                    .Include(a => a.Location)
                    .ToListAsync();

                return Ok(assets);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAssetById(int id)
        {
            try
            {
                var asset = await _context.Assets
                    .Include(a => a.AssetType)
                    .Include(a => a.CreatedByUser)
                    .Include(a => a.Department)
                    .Include(a => a.Location)
                    .FirstOrDefaultAsync(a => a.Id == id);

                if (asset == null)
                {
                    return NotFound(); // HTTP 404 Not Found if the item is not found
                }

                return Ok(asset); // HTTP 200 OK with the retrieved item
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Assets>> CreateAsset([FromBody] Assets assetInput)
        {
            // Validate the input model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Map the input model to the actual entity
                var asset = new Assets
                {
                    Aname = assetInput.Aname,
                    Quantity = assetInput.Quantity,
                    Cost = assetInput.Cost,
                    Shelve = assetInput.Shelve,
                    ImagePath = assetInput.ImagePath,
                    Locker = assetInput.Locker,
                    Date = DateTime.Now, // Set the date to the current time

                    // Set the foreign key properties
                    AssetTypeId = assetInput.AssetTypeId,
                    CreateByUserId = assetInput.CreateByUserId,
                    DId = assetInput.DId,
                    LocationId = assetInput.LocationId
                };

                // Add the entity to the context
                _context.Assets.Add(asset);

                // Save changes to the database
                await _context.SaveChangesAsync();

                // Return the created asset
                return CreatedAtAction(nameof(GetAssetById), new { id = asset.Id }, asset);
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return an error response
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsset(int id)
        {
            try
            {
                var asset = await _context.Assets.FindAsync(id);

                if (asset == null)
                {
                    return NotFound(); // HTTP 404 Not Found if the item is not found
                }

                _context.Assets.Remove(asset);
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
