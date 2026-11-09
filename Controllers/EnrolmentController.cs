using API_proj.Data;
using API_proj.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace API_proj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrolmentController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public EnrolmentController(ApplicationDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Registers a Participant for an Event (Participant Role Only).
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Participant")]
        public async Task<IActionResult> Enroll([FromBody] EnrolmentDto dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var participant = await _context.Participants.FirstOrDefaultAsync(p => p.UserId == userId);

            if (participant == null) return Unauthorized(new { message = "Participant profile not found." });

            var existing = await _context.Enrolments
                .FirstOrDefaultAsync(e => e.EventId == dto.EventId && e.ParticipantId == participant.ParticipantId);

            if (existing != null) return BadRequest(new { message = "Already enrolled in this event." });

            var enrollment = new Enrolment
            {
                EventId = dto.EventId,
                ParticipantId = participant.ParticipantId,
                Status = "Registered",
                EnrolledAt = DateTime.Now
            };

            _context.Enrolments.Add(enrollment);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Enrolled successfully." });
        }

        /// <summary>
        /// Returns all race enrolments for the logged-in participant.
        /// </summary>
        [HttpGet("mine")]
        [Authorize(Roles = "Participant")]
        public async Task<IActionResult> GetMyEnrollments()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var participant = await _context.Participants.FirstOrDefaultAsync(p => p.UserId == userId);

            if (participant == null) return Unauthorized();

            var list = await _context.Enrolments
                .Include(e => e.Event)
                .Where(e => e.ParticipantId == participant.ParticipantId)
                .ToListAsync();

            return Ok(list);
        }

        /// <summary>
        /// Captures participant finish time and position (Organiser Role Only).
        /// </summary>
        [HttpPost("results")]
        [Authorize(Roles = "Organiser")]
        public async Task<IActionResult> PostResult([FromBody] ResultDto dto)
        {
            var result = new Result
            {
                EventId = dto.EventId,
                ParticipantId = dto.ParticipantId,
                Position = dto.Position,
                FinishTime = TimeSpan.Parse(dto.FinishTime),
                CreatedAt = DateTime.Now
            };

            _context.Results.Add(result);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Race result recorded successfully." });
        }
    }
}
