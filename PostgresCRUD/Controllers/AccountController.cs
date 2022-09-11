using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostgresCRUD.Models;
using PostgresCRUD.DataAccess;
using PostgresCRUD.DTOs;
using PostgresCRUD.Interfaces;
using Microsoft.AspNetCore.Cors;

namespace PostgresCRUD.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly postgresContext _context;
        private readonly ITokenService _tokenService;

        public AccountController([Service] postgresContext context, ITokenService tokenService)
        {
             _context = context;
             _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.Username)) return BadRequest("UserName Is Already Taken");
            var hmac = new HMACSHA512();

            var user = new Appuser
            {
                Username = registerDto.Username.ToLower(),
                Passwordhash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                Passwordsalt= hmac.Key,

            };

            _context.Appusers.Add(user);
            await _context.SaveChangesAsync();

            return new UserDto
            {
                Username = user.Username,
                Token = _tokenService.CreateToken(user)
            };
        }



        private async Task <bool>UserExists(string username)
        {
            return await _context.Appusers.AnyAsync(x => x.Username == username.ToLower());
        }

        [HttpPost("login")]
        public async Task <ActionResult<UserDto>>Login(LoginDto loginDto)
        {
            var user = await _context.Appusers
                .SingleOrDefaultAsync(x => x.Username == loginDto.Username);

            if (user == null) return Unauthorized("Invalid UserName");

             var hmac = new HMACSHA512(user.Passwordsalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for(int i=0;i<computedHash.Length;i++)
            {
                if (computedHash[i] != user.Passwordhash[i]) return Unauthorized("Invalid Password");
            }

            return new UserDto
            {
                Username = user.Username,
                Token = _tokenService.CreateToken(user)
            };
        }

    }
}