namespace BlogBackend.Models
{
    public class Post
    {
        public int Id { get; set; } //Clave primaria
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Relaciones
        public List<Comment> Comments { get; set; } = new();
        public List<Like> Likes { get; set; } = new();
        public int? UserId { get; set; } // Clave foránea ( para cuando genere usuarios)
        public User? Author { get; set; }
    }
}
