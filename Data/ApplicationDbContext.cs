using Microsoft.EntityFrameworkCore;
using TestWebASP.NET.Models;

namespace TestWebASP.NET.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Character> Characters { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Franchise> Franchises { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Character>().HasData(
                new Character { FullName = "Geralt", Gender = "Male", Alias = "Geralt of Rivia", Id = 1, },
                new Character { FullName = "Tony Stark", Gender = "Male", Alias = "Iron Man", Id = 2 },
                new Character { FullName = "Peter Parker", Gender = "Male", Alias = "Spiderman", Id = 3 }
              );

            modelBuilder.Entity<Franchise>().HasData(
               new Franchise { Id = 1, Name = "Horror", Description = "Scary movie" },
               new Franchise { Id = 2, Name = "Marvel" }
             );

            modelBuilder.Entity<Movie>().HasData(
               new Movie { Id = 1, MovieTitle = "Witcher", FranchiseId = 1 },
               new Movie { Id = 2, MovieTitle = "Iron Man", FranchiseId = 2 },
               new Movie { Id = 3, MovieTitle = "Iron Man 2", FranchiseId = 2 }
             );

            //modelBuilder.Entity<Movie>()
            //    .HasMany(m => m.Characters)
            //    .WithMany(c => c.Movies)
            //    .UsingEntity<Dictionary<string, object>>("MovieCharacter",
            //    r => r.HasOne<Character>().WithMany().HasForeignKey("CharacterId"),
            //    l => l.HasOne<Movie>().WithMany().HasForeignKey("MovieId"),
            //    j =>
            //    {
            //        j.HasKey("CharacterId", "MovieId");
            //        j.HasData(
            //             new { CharacterId = 1, MovieId = 1 },
            //             new { CharacterId = 2, MovieId = 2 },
            //             new { CharacterId = 2, MovieId = 3 }
            //     );
            //    });

            modelBuilder.Entity<Movie>()
                 .HasMany(m => m.Characters)
                 .WithMany(c => c.Movies)
                 .UsingEntity(j => j.HasData(
                 new { CharactersId = 1, MoviesId = 1 },
                 new { CharactersId = 2, MoviesId = 2 },
                 new { CharactersId = 2, MoviesId = 3 }
     ));
        }
    }
}

