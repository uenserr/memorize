using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Memorize.Server.Data;
using Memorize.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibraryController : ControllerBase
    {
        private readonly ApiDbContext _context;
        public LibraryController(ApiDbContext context)
        {
            _context = context;
            
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrentUserDecks()
        {
            Console.WriteLine("GetDecks");
            var currentUser = await _context.Users.FirstOrDefaultAsync(p => p.Email == HttpContext.User.Identity.Name);
            var decks =  _context.Decks.Where(p => p.UserID == currentUser.ID);
            return Ok(decks);
        }

        
    }
}