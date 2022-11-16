using BanGiay.Data;
using BanGiay.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace BanGiay.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public static User user = new User();
        private readonly BanGiayContext _context;
        private readonly IConfiguration _configuration;

        public UsersController(IConfiguration configuration, BanGiayContext context)
        {
            configuration = _configuration;
            _context = context;
        }
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.ID = Guid.NewGuid();
            user.UserName = request.UserName;
            user.PasswordSalt = passwordSalt;
            user.PasswordHast = passwordHash;
           await  _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return Ok(user);

        }
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            if(user.UserName != request.UserName)
            {
                return BadRequest("Khong co ten nguoi dung nay !");
            }
            if (!VerifyPasswordHash(request.Password, user.PasswordHast, user.PasswordSalt))
            {
                return BadRequest("Mat khau sau");
            }
            string token =  CreateToken(user);
            return Ok(token);
        }
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.UserName)
        };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(("3wqp24giBSdROY3Juf")));
            var cread = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims:claims,
                expires:DateTime.Now.AddDays(8),
                signingCredentials:cread);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
        private void CreatePasswordHash(string password,out byte[] passwordHash,out byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash,  byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(user.PasswordSalt))
            {
              var  computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        
        }
    }
}
