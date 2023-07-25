using Loans.Data;
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
            var user = await _context.User.FirstOrDefaultAsync(x => x.email == userobj.email && x.password == userobj.password);
            if (user == null)
            {
                return Ok(new 
                { 
                    Message = "False"
                });
            }
           // user.Token = createJwt(user);
            return Ok(new
            {
                Message = "True"
               // Token = user.Token
            });
        }


        [HttpPost("admin/signup")]
        public async Task<IActionResult> saveAdmin([FromBody] AdminModel adminobj)
        {
            if (adminobj == null)
            {
                return BadRequest();
            }
            //  if (await CheckEmailExistAdmin(adminobj.email)) return BadRequest(new { Message = "Email Already Exist!!! " });
            //  if (await CheckEmailExistUser(adminobj.email)) return BadRequest(new { Message = "Email Already Exist!!! " });
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

            return Created("", true);
        }
    }
}
       



            
        
    

        //  private string createJwt(UserModel user)
        //  {
        //      if (user == null)
        //      {
        //          throw new ArgumentNullException(nameof(user), "User cannot be null");
        //      }

        //     var jwtTokenHandler = new JwtSecurityTokenHandler();
        //     var key = Encoding.ASCII.GetBytes("veryverysecret.....");
        //      var identity = new ClaimsIdentity(new Claim[]
        //      {
        //  new Claim(ClaimTypes.Role, user.userRole),
        //  new Claim(ClaimTypes.Email, user.email),
        //  new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user.ID))
        //      });
        //      var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
        //      var tokenDescriptor = new SecurityTokenDescriptor
        //      {
        //         Subject = identity,
        //         Expires = DateTime.Now.AddDays(1),
        //          SigningCredentials = credentials
        //     };
        //     var token = jwtTokenHandler.CreateToken(tokenDescriptor);
        //      return jwtTokenHandler.WriteToken(token);
        //  }


        //  private Task<bool> CheckEmailExistUser(string Email)
        //  {
        //      return (_context.User.AnyAsync(x => x.email == Email));
        //  }
        //  private Task<bool> CheckEmailExistAdmin(string Email)
        //  {
        //      return (_context.Admin.AnyAsync(x => x.email == Email));
        //  }