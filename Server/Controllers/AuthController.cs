using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Memorize.Server.Data;
using Memorize.Shared.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApiDbContext _context;
        public AuthController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet("getCurrentUser")]
        public async Task<IActionResult> GetCurrentUser()
        {
            Console.WriteLine("getUserHere");
            var user = await _context.Users.FirstOrDefaultAsync(p => p.Login == HttpContext.User.Identity.Name);
            if (user is null) return Unauthorized();
            return Ok(user);
        }

        [HttpPost()]
        public async Task<IActionResult> SignIn(User user)
        {
            var userExist = await _context.Users
                .FirstOrDefaultAsync(p => p.Login == user.Login && p.Password == user.Password);
            if (userExist is null) return Unauthorized();
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, userExist.Login) };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return Ok(userExist);
            
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return NoContent();
        }
    }
}