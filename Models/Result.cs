using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_proj.Models
{
    public class Result
    {
        public int ResultId { get; set; }


        public int EventId { get; set; }


        public Event? Event { get; set; }


        public int ParticipantId { get; set; }


        public Participant? Participant { get; set; }


        public int Position { get; set; }


        public TimeSpan FinishTime { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.Now;


    }
}
