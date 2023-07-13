using Loan.Data;
using Loan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace User.Controller
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public UserController(ApplicationDbContext applicationdbcontext)
        {
            _context = applicationdbcontext;
        }
        [HttpPost("user/addProfile")]
        public async Task<IActionResult> addUser([FromBody] UserModel userobj)
        {
            if (userobj == null)
            {
                return BadRequest();
            }

            await _context.User.AddAsync(userobj);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "User profile created"
            });
        }

        [HttpGet("user/getProfile/{ID}")]
        public async Task<IActionResult> getProfile(int ID)
        {
            var Userprofile = await _context.User.FirstOrDefaultAsync(p => p.ID == ID);

            if (Userprofile == null)
            {
                return NotFound(new
                {
                    Message = "No user found"
                });
            }

            return Ok(new
            {
                Message = "View Profile",
                UserDetails = Userprofile
            });

        }
        [HttpGet("user/getProfile")]
        public async Task<IActionResult> getProfiles()
        {
            var Userprofile = await _context.User.ToListAsync();

            if (Userprofile == null || Userprofile.Count == 0)
            {
                return NotFound(new
                {
                    Message = "No user found"
                });
            }

            return Ok(new
            {
                Message = "View Profile",
                UserDetails = Userprofile
            });

        }
        [HttpDelete("user/deleteProfile/{ID}")]
        public async Task<IActionResult> deleteUser(int ID)
        {
            var Userprofile = await _context.User.FindAsync(ID);

            if (Userprofile == null)
            {
                return NotFound();
            }

            _context.User.Remove(Userprofile);
            await _context.SaveChangesAsync();


            return Ok(new
            {
                Message = "Delete profile",
                UserDetails = Userprofile
            });
        }
        [HttpPut("user/deleteProfile/{ID}")]
        public async Task<IActionResult> EditUserProfile(int ID, [FromBody] UserModel userModel)
        {
            if (userModel == null || ID != userModel.ID)
            {
                return BadRequest();
            }

            var Userprofile = await _context.User.FindAsync(ID);

            if (Userprofile == null)
            {
                return NotFound();
            }

            Userprofile.email = userModel.email;
            Userprofile.password = userModel.password;
            Userprofile.username = userModel.username;
            Userprofile.mobileNumber = userModel.mobileNumber;
            Userprofile.userRole = userModel.userRole;


            _context.User.Update(Userprofile);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Updated profile",
                UserDetails = Userprofile
            });
        }
    }
}
   
