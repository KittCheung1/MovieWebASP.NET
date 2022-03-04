using MovieWebASP.NET.DTO.Requests;
using MovieWebASP.NET.DTO.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieWebASP.NET.Services
{
    public interface IMovieService
    {
        public Task<IEnumerable<ReadMovieDTO>> GetAllMoviesAsync();
        public Task<ReadMovieDTO> GetMovieAsync(int id);
        public Task<IEnumerable<ReadCharacterDTO>> GetAllCharacterInMovieAsync(int id);
        public Task<ReadMovieDTO> CreateMovieAsync(CreateMovieDTO createMovie);
        public Task<bool> UpdateMovieAsync(UpdateMovieDTO updateMovie, int id);
        public Task<bool> UpdateCharactersToMovieAsync(int id, List<int> characterIds);
        public Task<bool> DeleteMovieAsync(int id);
    }
}
