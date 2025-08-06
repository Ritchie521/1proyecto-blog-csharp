using Microsoft.EntityFrameworkCore;
using BlogBackend.Models;

namespace BlogBackend.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    // Tablas de la base de datos
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configura nombres de tablas personalizados
        modelBuilder.Entity<Post>().ToTable("Posts");
        modelBuilder.Entity<Comment>().ToTable("Comments");
        modelBuilder.Entity<Like>().ToTable("Likes");
        modelBuilder.Entity<User>().ToTable("Users");

        // Configuración de relaciones (opcional si usas convenciones estándar)
        modelBuilder.Entity<Post>()
            .HasMany(p => p.Comments)
            .WithOne(c => c.Post)
            .OnDelete(DeleteBehavior.Cascade); // Si borras un Post, se borran sus Comments

        modelBuilder.Entity<Like>()
            .HasOne(l => l.Post)
            .WithMany(p => p.Likes)
            .OnDelete(DeleteBehavior.Cascade);

        // índices para mejorar performance:
        modelBuilder.Entity<Post>()
            .HasIndex(p => p.CreatedAt);  // Útil para ordenar posts por fecha

        modelBuilder.Entity<Comment>()
            .HasIndex(c => c.PostId);     // Acelera búsquedas por PostId
    }
}
