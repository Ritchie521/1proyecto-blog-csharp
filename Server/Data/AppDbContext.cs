using Microsoft.EntityFrameworkCore;
using BlogBackend.Models;

namespace BlogBackend.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // Tablas de la base de datos
    public DbSet<Post> Posts { get; set; }
    // Añade más DbSets aquí si necesitas (ejem: DbSet<Comment> Comments)
}
