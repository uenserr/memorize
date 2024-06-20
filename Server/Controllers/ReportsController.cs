using Memorize.Server.Data;
using Memorize.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Memorize.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly ApiDbContext _context;
        public ReportsController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet()]
        public async Task<ActionResult<List<Report>>> GetAllReports()
        {
            var users = await _context.Reports.ToListAsync();
            if (users == null) return NotFound();
            return Ok(users);
        }
     

        [HttpPost]
        public async Task<ActionResult<Report>> CreateReport(Report report)
        {
            _context.Reports.Add(report);
            await _context.SaveChangesAsync();
            return Ok(report);
        }

        [HttpPut()]
        public async Task<IActionResult> UpdateReport(Report report)
        {
            var reportExists = await _context.Reports.FirstOrDefaultAsync(x => x.ID == report.ID);
            if (reportExists == null) return NotFound();
            reportExists.Message = report.Message;
            reportExists.IsHandled = report.IsHandled;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReport(int id)
        {
            var reportExists = await _context.Reports.FirstOrDefaultAsync(x => x.ID == id);
            if (reportExists == null) return NotFound();

            _context.Reports.Remove(reportExists);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
