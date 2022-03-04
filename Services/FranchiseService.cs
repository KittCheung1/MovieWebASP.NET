using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWebASP.NET.Data;
using TestWebASP.NET.DTO.Franchise;
using TestWebASP.NET.DTO.Responses;
using TestWebASP.NET.Models;

namespace TestWebASP.NET.Services
{
    public class FranchiseService : IFranchiseService
    {
        private readonly ApplicationDbContext _dbcontext;
        private readonly IMapper _mapper;
        public FranchiseService(ApplicationDbContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }

        public async Task<ReadFranchiseDTO> CreateFranchiseAsync(CreateFranchiseDTO franchise)
        {
            Franchise dbFranchise = _mapper.Map<Franchise>(franchise);
            _dbcontext.Franchises.Add(dbFranchise);
            await _dbcontext.SaveChangesAsync();

            return _mapper.Map<ReadFranchiseDTO>(dbFranchise);
        }

        public async Task<bool> DeleteFranchiseAsync(int id)
        {
            var foundFranchiseToDelete = await _dbcontext.Franchises.FindAsync(id);
            if (foundFranchiseToDelete == null)
            {
                return false;
            }

            _dbcontext.Franchises.Remove(foundFranchiseToDelete);
            await _dbcontext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<ReadCharacterDTO>> GetAllCharactersInFranchiseAsync(int id)
        {
            var dbFranchise = await _dbcontext.Franchises.Include(f => f.Movies).ThenInclude(f => f.Characters).FirstOrDefaultAsync(f => f.Id == id);

            if (dbFranchise == null)
            {
                return null;
            }
            var movieCharacters = dbFranchise.Movies.SelectMany(x => x.Characters);
            var distinctCharacters = movieCharacters.GroupBy(x => x.Id).Select(x => x.First());

            return _mapper.Map<IEnumerable<ReadCharacterDTO>>(distinctCharacters);
        }

        public async Task<IEnumerable<ReadFranchiseDTO>> GetAllFranchisesAsync()
        {
            return _mapper.Map<List<ReadFranchiseDTO>>(await _dbcontext.Franchises.ToArrayAsync());
        }

        public async Task<IEnumerable<ReadMovieDTO>> GetAllMoviesInFranchiseAsync(int id)
        {
            var dbFranchise = await _dbcontext.Franchises.Include(f => f.Movies).FirstOrDefaultAsync(f => f.Id == id);
            if (dbFranchise == null)
            {
                return null;
            }
            return _mapper.Map<IEnumerable<ReadMovieDTO>>(dbFranchise.Movies.ToList());
        }

        public async Task<ReadFranchiseDTO> GetFranchiseAsync(int id)
        {
            var foundFranchise = await _dbcontext.Franchises.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);


            if (foundFranchise == null)
            {
                return null;
            }

            return _mapper.Map<ReadFranchiseDTO>(foundFranchise);
        }

        public async Task<bool> UpdateFranchiseAsync(UpdateFranchiseDTO updateFranchise, int id)
        {
            if (!await FranchiseExists(id))
            {
                return false;
            }
            Franchise dbFranchise = _mapper.Map<Franchise>(updateFranchise);
            dbFranchise.Id = id;
            _dbcontext.Entry(dbFranchise).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateMovieInFranchiseAsync(List<int> movieIds, int id)
        {
            var dbFranchise = await _dbcontext.Franchises.Include(f => f.Movies).FirstOrDefaultAsync(f => f.Id == id);

            if (dbFranchise == null)
            {
                return false;
            }

            foreach (var movieId in movieIds)
            {
                var tempMovie = await _dbcontext.Movies.FirstOrDefaultAsync(m => m.Id == movieId);
                dbFranchise.Movies.Add(tempMovie);

            }
            await _dbcontext.SaveChangesAsync();

            return true;
        }

        private async Task<bool> FranchiseExists(int id)
        {
            return (await GetFranchiseAsync(id)) != null;
        }
    }
}
