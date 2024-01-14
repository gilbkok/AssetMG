using AssetMG.Data;
using AssetMG.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AssetMG.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AssetMGDbContext _UsersContext;
        public UsersController(AssetMGDbContext userscontext)
        {
            _UsersContext = userscontext;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Users>> GetAllUsers()
        {
            return _UsersContext.Users;
        }
        [HttpPost]
        public void Add(Users user)
        {
            _UsersContext.Users.Add(user);
            _UsersContext.SaveChanges();
        }
        [HttpGet]
        [Route ("Uid")]
        public async Task<IActionResult> GetUserbyId([FromRoute] int uid)
        {
            var user = await _UsersContext.Users.FirstOrDefaultAsync(x => x.Uid == uid);
            if (user != null)
            { 
                return Ok(user);  
            }
            return NotFound("User not found!");
        }
    }
}
