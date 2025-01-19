using Disney_Characters.Models;
using Microsoft.EntityFrameworkCore;

public class CharacterContext : DbContext
{
    public CharacterContext() { }

    public CharacterContext(DbContextOptions<CharacterContext> options) : base(options) { }

    public DbSet<CharacterDto> Characters { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      
        optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb; Database=DisneyDb; Trusted_Connection=True; TrustServerCertificate=True");
    }
}
