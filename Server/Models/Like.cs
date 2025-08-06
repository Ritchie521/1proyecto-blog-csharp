namespace BlogBackend.Models
{
    public class Like
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        //Claves foráneas
        public int PostId { get; set; }
        public Post Post { get; set; }

        public int? UserId { get; set; }
        public User? Author { get; set; }
    }
}
