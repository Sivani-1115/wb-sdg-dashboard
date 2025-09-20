namespace WBPortal.Api.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public required Project Project { get; set; }

        public int UserId { get; set; }
        public required User User { get; set; }

        public required string Message { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
