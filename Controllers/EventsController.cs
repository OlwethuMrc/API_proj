using API_proj.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace API_proj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public EventsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Events (Open to all roles)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<API_proj.Models.Event>>> GetEvents()
        {
            return await _context.Events
                .Include(e => e.Category)
                .Include(e => e.Organiser)
                .ToListAsync();
        }

        // GET: api/Events/5
        [HttpGet("{id}")]
        public async Task<ActionResult<API_proj.Models.Event>> GetEvent(int id)
        {
            var raceEvent = await _context.Events
                .Include(e => e.Category)
                .Include(e => e.Organiser)
                .FirstOrDefaultAsync(e => e.EventId == id);

            if (raceEvent == null) return NotFound();
            return raceEvent;
        }

        // POST: api/Events (Organiser Only)
        [HttpPost]
        [Authorize(Roles = "Organiser")]
        public async Task<ActionResult<API_proj.Models.Event>> PostEvent([FromBody] API_proj.Models.Event raceEvent)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (int.TryParse(userIdString, out int userId))
            {
                var organiser = await _context.Organisers.FirstOrDefaultAsync(o => o.UserId == userId);
                if (organiser == null) return Unauthorized(new { message = "Organiser profile not found." });

                // Fixed: matches OrganizerId with 'z' from Event.cs model
                raceEvent.OrganizerId = organiser.OrganiserId;
            }

            _context.Events.Add(raceEvent);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEvent), new { id = raceEvent.EventId }, raceEvent);
        }

        // PUT: api/Events/5 (Organiser Owner Only)
        [HttpPut("{id}")]
        [Authorize(Roles = "Organiser")]
        public async Task<IActionResult> PutEvent(int id, [FromBody] API_proj.Models.Event raceEvent)
        {
            if (id != raceEvent.EventId) return BadRequest();

            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdString, out int userId)) return Unauthorized();

            var organiser = await _context.Organisers.FirstOrDefaultAsync(o => o.UserId == userId);

            // Fixed: matches OrganizerId with 'z'
            if (organiser == null || raceEvent.OrganizerId != organiser.OrganiserId)
                return Forbid();

            _context.Entry(raceEvent).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Events/5 (Organiser Owner Only)
        [HttpDelete("{id}")]
        [Authorize(Roles = "Organiser")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var raceEvent = await _context.Events.FindAsync(id);
            if (raceEvent == null) return NotFound();

            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdString, out int userId)) return Unauthorized();

            var organiser = await _context.Organisers.FirstOrDefaultAsync(o => o.UserId == userId);

            // Fixed: matches OrganizerId with 'z'
            if (organiser == null || raceEvent.OrganizerId != organiser.OrganiserId)
                return Forbid();

            _context.Events.Remove(raceEvent);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
