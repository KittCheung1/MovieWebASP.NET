using Microsoft.EntityFrameworkCore;
using TestWebASP.NET.Models;

namespace TestWebASP.NET.Data
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data source= N-SE-01-2936\SQLEXPRESS;Initial Catalog=xxxxxxx;Integrated Security=True");
        }

        DbSet<Character> Characters { get; set; }
        DbSet<Movie> Movies { get; set; }
        DbSet<Franchise> Franchises { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Character>().HasData(
                new Character { FullName = "Geralt", Gender = "Male", Alias = "Geralt of Rivia", Id = 1 },
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

            modelBuilder.Entity<Movie>().HasMany(m => m.Characters).WithMany(c => c.Movies).UsingEntity(j => j.HasData(
                new { CharactersId = 1, MoviesId = 1 }
                ));
        }
    }
}

