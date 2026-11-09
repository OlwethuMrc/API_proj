namespace API_proj.Models
{
    public class ResultDto
    {
        public int EventId { get; set; }
        public int ParticipantId { get; set; }
        public int Position { get; set; }
        public string FinishTime { get; set; } = string.Empty; // Format: "00:22:35"
    }
}

