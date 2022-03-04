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
            modelBuilder.Entity<Movie>()
                .HasOne(m => m.Franchise)
                .WithMany(f => f.Movies)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            modelBuilder.Entity<Character>().HasData(
                new Character
                {
                    Id = 1,
                    FullName = "John Wick",
                    Gender = "Male",
                    Alias = "Baba Yaga",
                    Picture = "https://en.wikipedia.org/wiki/John_Wick_(character)#/media/File:John_Wick_Keanu.jpeg"
                },
                new Character
                {
                    Id = 2,
                    FullName = "Tony Stark",
                    Gender = "Male",
                    Alias = "Iron Man",
                    Picture = "https://ironman.fandom.com/wiki/Marvel%27s_Iron_Man?file=P170620_v_v8_ba.jpg"
                },
                new Character
                {
                    Id = 3,
                    FullName = "Peter Parker",
                    Gender = "Male",
                    Alias = "Spiderman",
                    Picture = "https://ironman.fandom.com/wiki/Marvel_Studios:_Iron_Man_2?file=P3546118_v_v8_af.jpg"
                }
              );

            modelBuilder.Entity<Franchise>().HasData(
               new Franchise { Id = 1, Name = "Scare the **** out of you-Movies", Description = "Scary movie" },
               new Franchise { Id = 2, Name = "Marvel", Description = "Superheros fighting to save the world" }
             );

            modelBuilder.Entity<Movie>().HasData(
               new Movie
               {
                   Id = 1,
                   MovieTitle = "John Wick",
                   FranchiseId = 1,
                   Director = "Chad Stahelski",
                   Genre = "Action, Crime, Thriller",
                   ReleaseYear = 2014,
                   Picture = "https://witcher.fandom.com/wiki/The_Witcher_(TV_series)?file=Netflix+poster+s1.jpg",
                   Trailer = "https://www.youtube.com/watch?v=2AUmvWm5ZDQ&ab_channel=LionsgateMovies"
               },
               new Movie
               {
                   Id = 2,
                   MovieTitle = "Iron Man",
                   FranchiseId = 2,
                   Director = "Jon Favreau",
                   Genre = "Action, Adventure, Sci-Fi",
                   ReleaseYear = 2008,
                   Picture = "https://upload.wikimedia.org/wikipedia/en/0/02/Iron_Man_%282008_film%29_poster.jpg",
                   Trailer = "https://www.youtube.com/watch?v=8hYlB38asDY&ab_channel=TheMovieChanneI"
               },
               new Movie
               {
                   Id = 3,
                   MovieTitle = "Spiderman",
                   FranchiseId = 2,
                   Director = "Sam Raimi",
                   Genre = "Action, Adventure, Sci-Fi",
                   ReleaseYear = 2002,
                   Picture = "https://en.wikipedia.org/wiki/Spider-Man_(2002_film)#/media/File:Spider-Man2002Poster.jpg",
                   Trailer = "https://www.youtube.com/watch?v=_yhFofFZGcc&ab_channel=MovieclipsClassicTrailers"
               }
             );

            modelBuilder.Entity<Movie>()
                 .HasMany(m => m.Characters)
                 .WithMany(c => c.Movies)
                 .UsingEntity(j => j.HasData(
                 new { CharactersId = 1, MoviesId = 1 },
                 new { CharactersId = 2, MoviesId = 2 },
                 new { CharactersId = 3, MoviesId = 3 }
     ));
        }
    }
}

