namespace BlogBackend.Models
{
    public class Post
    {
        public int Id { get; set; } //Clave primaria
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
