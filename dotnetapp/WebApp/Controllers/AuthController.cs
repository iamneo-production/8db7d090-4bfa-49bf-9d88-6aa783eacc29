using bloans.Database;
using bloans.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;  
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace bloans.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly BloansDbContext _context;
        public AuthController(BloansDbContext applicationDbContext)

        {
            _context = applicationDbContext;

        }

        [HttpPost("admin/login")]
        public async Task<IActionResult> isAdminPresent([FromBody] LoginModel adminobj)
        {
            if (adminobj == null)
            {
                return BadRequest();
            }
            var admin = await _context.Admin.FirstOrDefaultAsync(x => x.email == adminobj.email && x.password == adminobj.password);
            if (admin == null)
            {
                return NotFound(new { Message = "False" });
            }
            return Ok(new { Message = "True" });
        }


        [HttpPost("user/login")]
        public async Task<IActionResult> isUserPresent([FromBody] LoginModel userobj)
        {
            if (userobj == null)
            {
                return BadRequest();
            }
            var users = await _context.User.FirstOrDefaultAsync(x => x.email == userobj.email && x.password == userobj.password);
            if (users == null)
            {
                return NotFound(new { Message = "False" });
            }
            return Ok(new { Message = "True" });
        }



        [HttpPost("admin/signup")]
        public async Task<IActionResult> saveAdmin([FromBody] AdminModel adminobj)
        {
            if (adminobj == null)
            {
                return BadRequest();
            }
            await _context.Admin.AddAsync(adminobj);
            await _context.SaveChangesAsync();
            var admin = new AdminModel
            {
                email = adminobj.email,
                password = adminobj.password,
                mobileNumber = adminobj.mobileNumber,
                userRole = adminobj.userRole
            };
            await _context.Admin.AddAsync(admin);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                Message = "Admin added"
            });
        }

        [HttpPost("user/signup")]
        public async Task<IActionResult> saveUser([FromBody] UserModel userobj)
        {
            if (userobj == null)
            {
                return BadRequest();
            }

            await _context.User.AddAsync(userobj);
            await _context.SaveChangesAsync();

            var loginObj = new LoginModel
            {
                email = userobj.email,
                password = userobj.password
            };

            await _context.AddAsync(loginObj);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "User added"
            });
        }

    }
}

