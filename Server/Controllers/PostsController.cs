using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlogBackend.Data;
using BlogBackend.Models;

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

        [HttpGet]
        public async Task<ActionResult<List<Post>>> GetPosts()
        {
            var posts = await _context.Posts.ToListAsync();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
            var post = await _context.Posts.FindAsync(id);

            if (post == null)
                return NotFound(); // Código 404 si no existe

            return Ok(post);
        }

        [HttpPost]
        public async Task<ActionResult<Post>> CreatePost(Post post)
        {
            if (!ModelState.IsValid) // Esto es redundante con [ApiController], pero se puede usar para lógica adicional
            {
                return BadRequest(ModelState);
            }

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
        }

        [HttpGet("{id}/comments")]
        public async Task<ActionResult<List<Comment>>> GetPostComments(int id)
        {
            // LINQ: Filtra comentarios por PostId
            var comments = await _context.Comments
                .Where(c => c.PostId == id)
                .ToListAsync();

            return Ok(comments);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleetePost(int id)
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
