using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_proj.Models
{
    public class Event
    {
        public int EventId { get; set; }


        public int OrganizerId { get; set; }


        public Organiser Organiser { get; set; }


        public int CategoryId { get; set; }


        public Category Category { get; set; }


        public string Name { get; set; } = string.Empty;


        public string Description { get; set; }


        public DateTime EventDate { get; set; }


        public string Location { get; set; } = string.Empty;


        public int MaxParticipants { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
