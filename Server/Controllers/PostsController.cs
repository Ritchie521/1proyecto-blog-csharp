using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlogBackend.Data;
using BlogBackend.Models;
using BlogBackend.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace BlogBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // la ruta base será "/api/posts"

    public class PostsController : ControllerBase
    {
        private readonly AppDbContext _context;
        // Inyección de dependencias

        public PostsController(AppDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<Post>>> GetPosts(CancellationToken ct)
        {
            var posts = await _context.Posts
                .AsNoTracking()
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();

            return Ok(posts);
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Post>> GetPost(int id, CancellationToken ct)
        {
            var post = await _context.Posts
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id, ct);

            if (post == null)
                return NotFound(); // Código 404 si no existe

            return Ok(post);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Post>> CreatePost([FromBody] CreatePostDto dto, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(dto.Title) || string.IsNullOrWhiteSpace(dto.Content)) // Esto es redundante con [ApiController], pero se puede usar para lógica adicional
                return BadRequest("Title y Content son obligatorios.");

            var post = new Post
            {
                Title = dto.Title.Trim(),
                Content = dto.Content.Trim(),
                CreatedAt = DateTime.UtcNow // Valor del servidor
            };

            _context.Posts.Add(post);
            await _context.SaveChangesAsync(ct);

            return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
        }

        [AllowAnonymous]
        [HttpGet("{id}/comments")]
        public async Task<ActionResult<List<Comment>>> GetPostComments(int id)
        {
            // LINQ: Filtra comentarios por PostId
            var comments = await _context.Comments
                .Where(c => c.PostId == id)
                .ToListAsync();

            return Ok(comments);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return NoContent(); // Código 204 (éxito sin contenido de retorno)
        }
    }

    
}
