namespace WBPortal.Api.DTOs
{
    public class UserResponseDTO
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Role { get; set; }
        public List<ProjectResponseDTO> Projects { get; set; } = new();
        public List<FeedbackResponseDTO> Feedbacks { get; set; } = new();
    }

    public class UserCreateDTO
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Role { get; set; }
    }

    public class UserUpdateDTO
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
        public string? Password { get; set; }
        public required string Role { get; set; }
    }
}
