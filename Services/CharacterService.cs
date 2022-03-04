using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestWebASP.NET.Data;
using TestWebASP.NET.DTO.Requests;
using TestWebASP.NET.DTO.Responses;
using TestWebASP.NET.Models;

namespace TestWebASP.NET.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly ApplicationDbContext _dbcontext;
        private readonly IMapper _mapper;

        public CharacterService(ApplicationDbContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;

        }

        public async Task<ReadCharacterDTO> CreateCharacterAsync(CreateCharacterDTO character)
        {
            Character dbCharacter = _mapper.Map<Character>(character);
            _dbcontext.Characters.Add(dbCharacter);
            await _dbcontext.SaveChangesAsync();

            return _mapper.Map<ReadCharacterDTO>(dbCharacter);
        }

        public async Task<bool> DeleteCharacterAsync(int id)
        {
            var foundCharacterToDelete = await _dbcontext.Characters.FindAsync(id);
            if (foundCharacterToDelete == null)
            {
                return false;
            }

            _dbcontext.Characters.Remove(foundCharacterToDelete);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ReadCharacterDTO>> GetAllCharactersAsync()
        {
            return _mapper.Map<List<ReadCharacterDTO>>(await _dbcontext.Characters.ToArrayAsync());
        }

        public async Task<ReadCharacterDTO> GetCharacterAsync(int id)
        {
            var foundCharacter = await _dbcontext.Characters.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);


            if (foundCharacter == null)
            {
                return null;
            }

            return _mapper.Map<ReadCharacterDTO>(foundCharacter);
        }

        public async Task<bool> UpdateCharacterAsync(UpdateCharacterDTO character, int id)
        {
            if (!await CharacterExists(id))
            {
                return false;
            }

            Character dbCharacter = _mapper.Map<Character>(character);
            dbCharacter.Id = id;
            _dbcontext.Entry(dbCharacter).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();

            return true;
        }

        private async Task<bool> CharacterExists(int id)
        {
            return (await GetCharacterAsync(id)) != null;
        }
    }
}
