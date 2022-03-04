using MovieWebASP.NET.DTO.Franchise;
using MovieWebASP.NET.DTO.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieWebASP.NET.Services
{
    public interface IFranchiseService
    {
        public Task<IEnumerable<ReadFranchiseDTO>> GetAllFranchisesAsync();
        public Task<ReadFranchiseDTO> GetFranchiseAsync(int id);
        public Task<IEnumerable<ReadMovieDTO>> GetAllMoviesInFranchiseAsync(int id);
        public Task<IEnumerable<ReadCharacterDTO>> GetAllCharactersInFranchiseAsync(int id);
        public Task<bool> UpdateFranchiseAsync(UpdateFranchiseDTO updateFranchise, int id);
        public Task<bool> UpdateMovieInFranchiseAsync(List<int> movieIds, int id);
        public Task<ReadFranchiseDTO> CreateFranchiseAsync(CreateFranchiseDTO franchise);
        public Task<bool> DeleteFranchiseAsync(int id);

    }
}
