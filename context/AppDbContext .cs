using API_spotify_att.Models;
using Microsoft.EntityFrameworkCore;

namespace API_spotify_att.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Musica> Musicas { get; set; } = null!;
    }
}
