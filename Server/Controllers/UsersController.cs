using Memorize.Server.Data;
using Memorize.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {

        private readonly ApiDbContext _context;
        public UsersController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet("user/{login}")]
        public async Task<ActionResult<User>> GetUserByLogin(string login)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Login == login);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpGet("param/{query}")]
        public async Task<ActionResult<List<User>>> GetUsersByName(string query)
        {
            var users = _context.Users.Where(x => x.Login.Contains(query));
            if (users == null) return NotFound();
            return Ok(users);
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }

        [HttpPut()]
        public async Task<IActionResult> UpdateUser(User user)
        {
            var userExists = await _context.Users.FirstOrDefaultAsync(x => x.ID == user.ID);
            if (userExists == null) return NotFound();
            userExists.Email = user.Email;
            userExists.Login = user.Login;
            userExists.Password = user.Password;
            userExists.Image = user.Image;

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}