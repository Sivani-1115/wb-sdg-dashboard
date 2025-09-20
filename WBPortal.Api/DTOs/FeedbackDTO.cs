namespace WBPortal.Api.DTOs
{
    public class FeedbackCreateDTO
    {
        public int ProjectId { get; set; }
        public required string Message { get; set; }
    }

    public class FeedbackResponseDTO
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public required string UserName { get; set; }
        public required string Message { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
