using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWebASP.NET.Data;
using TestWebASP.NET.DTO.Requests;
using TestWebASP.NET.DTO.Responses;
using TestWebASP.NET.Models;

namespace TestWebASP.NET.Services
{
    public class MovieService : IMovieService
    {
        private readonly ApplicationDbContext _dbcontext;
        private readonly IMapper _mapper;

        public MovieService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbcontext = dbContext;
            _mapper = mapper;
        }

        public async Task<bool> UpdateCharactersToMovieAsync(int id, List<int> characterIds)
        {
            var dbMovie = await _dbcontext.Movies.Include(m => m.Characters).FirstOrDefaultAsync(m => m.Id == id);
            if (dbMovie == null)
            {
                return false;
            }
            foreach (var character in characterIds)
            {
                var tempCharacter = await _dbcontext.Characters.FirstOrDefaultAsync(c => c.Id == character);
                dbMovie.Characters.Add(tempCharacter);
            }
            await _dbcontext.SaveChangesAsync();

            return true;
        }

        public async Task<ReadMovieDTO> CreateMovieAsync(CreateMovieDTO createMovie)
        {
            Movie dbMovie = _mapper.Map<Movie>(createMovie);
            _dbcontext.Movies.Add(dbMovie);
            await _dbcontext.SaveChangesAsync();

            return _mapper.Map<ReadMovieDTO>(dbMovie);
        }

        public async Task<bool> DeleteMovieAsync(int id)
        {

            var foundMovieToDelete = await _dbcontext.Movies.FindAsync(id);
            if (foundMovieToDelete == null)
            {
                return false;
            }

            _dbcontext.Movies.Remove(foundMovieToDelete);
            await _dbcontext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<ReadCharacterDTO>> GetAllCharacterInMovieAsync(int id)
        {
            var dbMovie = await _dbcontext.Movies.Include(m => m.Characters).FirstOrDefaultAsync(m => m.Id == id);

            if (dbMovie == null)
            {
                return null;
            }

            return _mapper.Map<IEnumerable<ReadCharacterDTO>>(dbMovie.Characters.ToList());
        }

        public async Task<IEnumerable<ReadMovieDTO>> GetAllMoviesAsync()
        {
            return _mapper.Map<List<ReadMovieDTO>>(await _dbcontext.Movies.ToArrayAsync());
        }

        public async Task<ReadMovieDTO> GetMovieAsync(int id)
        {
            var foundMovie = await _dbcontext.Movies.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

            if (foundMovie == null)
            {
                return null;
            }
            return _mapper.Map<ReadMovieDTO>(foundMovie);
        }

        public async Task<bool> UpdateMovieAsync(UpdateMovieDTO updateMovie, int id)
        {
            if (!await MovieExists(id))
            {
                return false;
            }
            Movie dbMovie = _mapper.Map<Movie>(updateMovie);
            dbMovie.Id = id;
            _dbcontext.Entry(dbMovie).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();

            return true;
        }

        private async Task<bool> MovieExists(int id)
        {
            return (await GetMovieAsync(id)) != null;
        }
    }
}
