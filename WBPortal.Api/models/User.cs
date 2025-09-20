namespace WBPortal.Api.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; } // hashed
        public required string Role { get; set; } // "Admin", "Manager", "User"

        // Navigation properties
        public ICollection<Project> Projects { get; set; } = new List<Project>();
        public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
    }
}
