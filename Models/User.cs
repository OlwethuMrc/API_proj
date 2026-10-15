using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_proj.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string Username { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;


        public string Role { get; set; } = string.Empty; // 'Organiser' or 'Participant'


        public string FirstName { get; set; } = string.Empty;


        public string LastName { get; set; } = string.Empty;


        public string Email { get; set; } = string.Empty;


        public string Phone { get; set; } = string.Empty;


        public DateTime CreatedAt { get; set; } = DateTime.Now;
    



}
}
    

