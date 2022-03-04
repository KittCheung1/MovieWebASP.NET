using System.Collections.Generic;
using System.Threading.Tasks;
using TestWebASP.NET.DTO.Requests;
using TestWebASP.NET.DTO.Responses;

namespace TestWebASP.NET.Services
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
