namespace WBPortal.Api.DTOs
{
    public class ProjectResponseDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public int ProjectManagerId { get; set; }
        public required string ProjectManagerName { get; set; }
        public List<FeedbackResponseDTO> Feedbacks { get; set; } = new();
    }

    public class ProjectCreateDTO
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public int ProjectManagerId { get; set; }
    }

    public class ProjectUpdateDTO
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public int ProjectManagerId { get; set; }
    }
}
