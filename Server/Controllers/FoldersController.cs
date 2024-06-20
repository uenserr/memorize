using Memorize.Client.Services.Implementations;
using Memorize.Server.Data;
using Memorize.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Memorize.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FoldersController : ControllerBase
    {
        private readonly ApiDbContext _context;
        public FoldersController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<Folder>>> GetUserFolders(int userId)
        {
            var folders = await _context.Folders.Where(x => x.UserID == userId).ToListAsync();
            if (folders == null) return NotFound();
            return Ok(folders);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Folder>> GetFolder(int id)
        {
            var card = _context.Folders.FirstOrDefault(x => x.ID == id);
            if (card == null) return NotFound();
            return Ok(card);
        }

        [HttpGet("param/{query}")]
        public async Task<ActionResult<List<Folder>>> GetFoldersByName(string query)
        {
            var folders = _context.Folders.Where(x => x.Name.Contains(query));
            if (folders == null) return NotFound();
            return Ok(folders);
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateFolder([FromBody] Folder folder)
        {
            _context.Folders.Add(folder);
            await _context.SaveChangesAsync();
            return Ok(folder);
        }

        [HttpPut()]
        public async Task<IActionResult> UpdateFolder(Folder folder)
        {
            var folderExists = await _context.Folders.FirstOrDefaultAsync(x => x.ID == folder.ID);
            if (folderExists == null) return NotFound();
            folderExists.Name = folder.Name;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFolder(int id)
        {
            var folderExists = await _context.Folders.FirstOrDefaultAsync(x => x.ID == id);
            if (folderExists == null) return NotFound();

            _context.Folders.Remove(folderExists);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}