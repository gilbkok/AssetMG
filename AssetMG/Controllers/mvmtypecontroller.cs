using AssetMG.Data;
using AssetMG.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace AssetMG.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class mvmtypecontroller : ControllerBase
    {
        private readonly AssetMGDbContext _MvmtypeContext;
        public mvmtypecontroller(AssetMGDbContext mvmtypeContext)
        {
            _MvmtypeContext = mvmtypeContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllMovementTypes()
        {
            try
            {
                var movementTypes = await _MvmtypeContext.MvmtTypes.ToListAsync();
                return Ok(movementTypes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovementTypeById(int id)
        {
            try
            {
                var movementType = await _MvmtypeContext.MvmtTypes.FindAsync(id);

                if (movementType == null)
                {
                    return NotFound(); // HTTP 404 Not Found if the item is not found
                }

                return Ok(movementType); // HTTP 200 OK with the retrieved item
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovementType([FromBody] Asset_Mvmt_Type newMovementType)
        {
            try
            {
                _MvmtypeContext.MvmtTypes.Add(newMovementType);
                await _MvmtypeContext.SaveChangesAsync();

                return CreatedAtAction(nameof(GetMovementTypeById), new { id = newMovementType.AMId }, newMovementType);
                // HTTP 201 Created with the newly created item and its location in the header
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovementType(int id, [FromBody] Asset_Mvmt_Type updatedMovementType)
        {
            try
            {
                if (id != updatedMovementType.AMId)
                {
                    return BadRequest("ID mismatch"); // HTTP 400 Bad Request if ID doesn't match
                }

                _MvmtypeContext.Entry(updatedMovementType).State = EntityState.Modified;
                await _MvmtypeContext.SaveChangesAsync();

                return NoContent(); // HTTP 204 No Content on successful update
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovementType(int id)
        {
            try
            {
                var movementType = await _MvmtypeContext.MvmtTypes.FindAsync(id);

                if (movementType == null)
                {
                    return NotFound(); // HTTP 404 Not Found if the item is not found
                }

                _MvmtypeContext.MvmtTypes.Remove(movementType);
                await _MvmtypeContext.SaveChangesAsync();

                return NoContent(); // HTTP 204 No Content on successful deletion
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
