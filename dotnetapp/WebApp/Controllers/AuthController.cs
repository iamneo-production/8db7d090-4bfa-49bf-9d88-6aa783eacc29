using Aloans.Data;
using Aloans.Model;
using Aloans.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;                          

namespace Aloans.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AloansDbContext _context;
        public AuthController(AloansDbContext applicationDbContext)

        {
            _context = applicationDbContext;

        }
        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody] LoginModel obj)
        {
            if (obj == null) return BadRequest();
            var user = await _context.User.FirstOrDefaultAsync(x => x.email == obj.email && x.password == obj.password);
            var admin = await _context.Admin.FirstOrDefaultAsync(x => x.email == obj.email && x.password == obj.password);

            if (user != null)
            {
                user.Token = createJwt(user);
                return Ok(new { Message = "User Logged in Successfully!", Token = user.Token });
            }
            else if (admin != null)
            {
                admin.Token = createJwt(admin);
                return Ok(new { Message = "Admin Logged in Successfully!", Token = admin.Token });
            }
            else return NotFound();
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
            await _context.LoginModels.AddAsync(loginObj);
            await _context.SaveChangesAsync();
            // return Created("User added");
            // Replace with your actual URI format

            return Created("", true);
        }

        [HttpPost("admin/signup")]
        public async Task<IActionResult> saveAdmin([FromBody] AdminModel adminObj)
        {
            if (adminObj == null) return BadRequest(new { Message = "admin is null" });
            if (await CheckEmailExistAdmin(adminObj.email)) return BadRequest(new { Message = "Email Already Exist!!! " });
            if (await CheckEmailExistUser(adminObj.email)) return BadRequest(new { Message = "Email Already Exist!!! " });


            if (await CheckMobileExistUser(adminObj.mobileNumber)) return BadRequest(new { Message = "Mobile Number Already Exist!!! " });
            if (await CheckMobileExistAdmin(adminObj.mobileNumber)) return BadRequest(new { Message = "Mobile number  Already Exist!!! " });
            await _context.Admin.AddAsync(adminObj);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Admin Registered :)" });
        }
        private string createJwt(UserModel user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("veryverysecret.....");
            var identity = new ClaimsIdentity(new Claim[]
            {
            new Claim(ClaimTypes.Role, user.userRole),
            new Claim(ClaimTypes.Email,user.email),
            new Claim(ClaimTypes.NameIdentifier,Convert.ToString(user.ID))
            });
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }
        private string createJwt(AdminModel admin)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("veryverysecret.....");
            var identity = new ClaimsIdentity(new Claim[]
            {
            new Claim(ClaimTypes.Role, admin.userRole),
            new Claim(ClaimTypes.NameIdentifier,Convert.ToString(admin.Id))
            });
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }
        private Task<bool> CheckEmailExistUser(string Email)
        {
            return (_context.User.AnyAsync(x => x.email == Email));
        }
        private Task<bool> CheckEmailExistAdmin(string Email)
        {
            return (_context.Admin.AnyAsync(x => x.email == Email));
        }
        private Task<bool> CheckMobileExistUser(string mobile )
        {
            return (_context.User.AnyAsync(x => x.mobileNumber == mobile));
        }
        private Task<bool> CheckMobileExistAdmin(string mobile)
        {
            return (_context.User.AnyAsync(x => x.mobileNumber == mobile));
        }
    }
}
