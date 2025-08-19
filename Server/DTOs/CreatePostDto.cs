using System.ComponentModel.DataAnnotations;

namespace BlogBackend.DTOs
{
    public class CreatePostDto
    {
        [Required, StringLength(255)]
        public string Title { get; set; } = string.Empty;

        [Required, StringLength(10000)]
        public string Content {  get; set; } = string.Empty;
    }
}
