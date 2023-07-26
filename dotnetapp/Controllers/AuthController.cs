using Loans.Context;
using Loans.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth.Controller
{
    [ApiController]


    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public AuthController(ApplicationDbContext applicationDbContext)
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


        [HttpPost("admin/signup")]
        public async Task<IActionResult> saveAdmin([FromBody] AdminModel adminobj)
        {
            if (adminobj == null)
            {
                return BadRequest();
            }
             if (await CheckEmailExistAdmin(adminobj.email)) return BadRequest(new { Message = "Email Already Exist!!! " });
             if (await CheckEmailExistUser(adminobj.email)) return BadRequest(new { Message = "Email Already Exist!!! " });
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
            await _context.Login.AddAsync(loginObj);
            await _context.SaveChangesAsync();
            // return Created("User added");
            // Replace with your actual URI format

            return Created("", true);
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
            new Claim(ClaimTypes.NameIdentifier,Convert.ToString(admin.ID))
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
    }
}