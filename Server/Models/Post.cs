using System.ComponentModel.DataAnnotations;
namespace BlogBackend.Models
{
    public class Post
    {
        public int Id { get; set; } //Clave primaria

        [Required(ErrorMessage = "El título es obligatorio")]
        [StringLength(100, ErrorMessage = "El título no puede exceder 100 caracteres")]
        public string Title { get; set; } = string.Empty; // Valor por defecto

        [Required(ErrorMessage = "El contenido es obligatorio")]
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Relaciones
        public List<Comment> Comments { get; set; } = new();
        public List<Like> Likes { get; set; } = new();
        public int? UserId { get; set; } // Clave foránea ( para cuando genere usuarios)
        public User? Author { get; set; }
    }
}
