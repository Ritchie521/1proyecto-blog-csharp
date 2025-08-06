namespace BlogBackend.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Claves foráneas
        public int PostId { get; set; }
        public Post Post { get; set; }

        public int? UserId { get; set; }
        public User? Author { get; set; } // (para cuando se cree el usuario)

    }
}
