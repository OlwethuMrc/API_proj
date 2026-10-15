using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_proj.Models
{
    public class Participant
    {
        public int ParticipantId { get; set; }


        public int UserId { get; set; }


        public User? User { get; set; }


        public DateTime DateOfBirth { get; set; }


        public string Gender { get; set; } = string.Empty;


        public DateTime CreatedAt { get; set; } = DateTime.Now;


    }
}
