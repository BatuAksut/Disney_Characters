using Disney_Characters.Models;
using Disney_Characters.Models.Disney_Characters.Models;
using Microsoft.EntityFrameworkCore;

namespace Disney_Characters
{
   
    public class DisneyDbContext : DbContext
    {
        public DisneyDbContext(DbContextOptions<DisneyDbContext> options) : base(options)
        {
        }

        public DbSet<Character> Characters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Character>(entity =>
            {
                // Id alanını identity olmaktan çıkar
                entity.Property(e => e.Id)
                    .ValueGeneratedNever();

                entity.Property(e => e.Films)
                    .HasConversion(
                        v => string.Join(',', v),
                        v => string.IsNullOrEmpty(v)
                            ? new List<string>()
                            : v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList())
                    .HasDefaultValue(new List<string>());

                entity.Property(e => e.TvShows)
                    .HasConversion(
                        v => string.Join(',', v),
                        v => string.IsNullOrEmpty(v)
                            ? new List<string>()
                            : v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList())
                    .HasDefaultValue(new List<string>());
            });
        }
    }
}
