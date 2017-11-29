using Microsoft.EntityFrameworkCore;

namespace Lab9.Models
{
    public class SongContext : DbContext
    {
        public SongContext(DbContextOptions<SongContext> options)
                : base(options)
        {
        }

        public DbSet<Song> Song { get; set; }
    }
}