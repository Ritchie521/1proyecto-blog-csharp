using System.ComponentModel.DataAnnotations;

namespace BlogBackend.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required, StringLength(40)]
        public string Username { get; set; } = string.Empty;

        [Required, EmailAddress, StringLength(120)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        // Role simple: "Admin" o "User"
        [Required, StringLength(20)]
        public string Role { get; set; } = "User";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Relaciones
        public List<Post> Posts { get; set; } = new();
        public List<Comment> Comments { get; set; } = new();
        public List<Like> Likes { get; set; } = new();
    }
}
