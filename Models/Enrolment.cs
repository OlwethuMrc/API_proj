using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_proj.Models
{
    public class Enrolment
    {
        [Key] 
        public int EnrollmentId { get; set; }



        public int EventId { get; set; }


        public Event Event { get; set; }


        public int ParticipantId { get; set; }


        public Participant Participant { get; set; }


        public DateTime EnrolledAt { get; set; } = DateTime.Now;


        public string Status { get; set; } = "Registered";



    }
}
