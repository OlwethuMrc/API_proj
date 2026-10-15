using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_proj.Models
{
    public class Organiser
    {
        public int OrganiserId { get; set; }


        public int UserId { get; set; }


        public User? User { get; set; }


        public string OrganisationName { get; set; } = string.Empty;


        public DateTime CreatedAt { get; set; } = DateTime.Now;



    }
}

