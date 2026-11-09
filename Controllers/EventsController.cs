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

        /// <summary>
        /// Returns all events (Open to Organisers and Participants).
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<API_proj.Models.Event>>> GetEvents()
        {
            return await _context.Events
                .Include(e => e.Category)
                .Include(e => e.Organiser)
                .ToListAsync();
        }

        /// <summary>
        /// Returns an event by ID.
        /// </summary>
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

        /// <summary>
        /// Creates a new race event (Organiser Role Only).
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Organiser")]
        public async Task<ActionResult<API_proj.Models.Event>> PostEvent([FromBody] API_proj.Models.Event raceEvent)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var organiser = await _context.Organisers.FirstOrDefaultAsync(o => o.UserId == userId);

            if (organiser == null) return Unauthorized(new { message = "Organiser account not found." });

            raceEvent.OrganizerId = organiser.OrganiserId;
            _context.Events.Add(raceEvent);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEvent), new { id = raceEvent.EventId }, raceEvent);
        }

        /// <summary>
        /// Updates an existing event (Organiser Owner Only).
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Organiser")]
        public async Task<IActionResult> PutEvent(int id, [FromBody] API_proj.Models.Event raceEvent)
        {
            if (id != raceEvent.EventId) return BadRequest();

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var organiser = await _context.Organisers.FirstOrDefaultAsync(o => o.UserId == userId);

            if (organiser == null || raceEvent.OrganizerId != organiser.OrganiserId)
                return Forbid();

            _context.Entry(raceEvent).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Deletes an event (Organiser Owner Only).
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Organiser")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var raceEvent = await _context.Events.FindAsync(id);
            if (raceEvent == null) return NotFound();

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var organiser = await _context.Organisers.FirstOrDefaultAsync(o => o.UserId == userId);

            if (organiser == null || raceEvent.OrganizerId != organiser.OrganiserId)
                return Forbid();

            _context.Events.Remove(raceEvent);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
