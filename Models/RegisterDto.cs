namespace API_proj.Models
{
    public class RegisterDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty; // 'Organiser' or 'Participant'
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        // Optional extra fields for specific roles
        public string? OrganisationName { get; set; } // Required if Organiser
        public DateTime? DateOfBirth { get; set; }      // Required if Participant
        public string? Gender { get; set; }             // Required if Participant
    }
}
