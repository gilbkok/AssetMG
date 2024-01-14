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
    public class DepartmentController : ControllerBase
    {
        private readonly AssetMGDbContext _context;

        public DepartmentController(AssetMGDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDepartments()
        {
            try
            {
                var departments = await _context.Department.ToListAsync();
                return Ok(departments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            try
            {
                var department = await _context.Department.FindAsync(id);

                if (department == null)
                {
                    return NotFound(); // HTTP 404 Not Found if the item is not found
                }

                return Ok(department); // HTTP 200 OK with the retrieved item
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromBody] Department newDepartment)
        {
            try
            {
                _context.Department.Add(newDepartment);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetDepartmentById), new { id = newDepartment.DId }, newDepartment);
                // HTTP 201 Created with the newly created item and its location in the header
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] Department updatedDepartment)
        {
            try
            {
                if (id != updatedDepartment.DId)
                {
                    return BadRequest("ID mismatch"); // HTTP 400 Bad Request if ID doesn't match
                }

                _context.Entry(updatedDepartment).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent(); // HTTP 204 No Content on successful update
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            try
            {
                var department = await _context.Department.FindAsync(id);

                if (department == null)
                {
                    return NotFound(); // HTTP 404 Not Found if the item is not found
                }

                _context.Department.Remove(department);
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
