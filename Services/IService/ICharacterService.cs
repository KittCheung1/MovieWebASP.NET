using System.Collections.Generic;
using System.Threading.Tasks;
using TestWebASP.NET.DTO.Requests;
using TestWebASP.NET.DTO.Responses;

namespace TestWebASP.NET.Services
{
    public interface ICharacterService
    {
        public Task<IEnumerable<ReadCharacterDTO>> GetAllCharactersAsync();
        public Task<ReadCharacterDTO> GetCharacterAsync(int id);
        public Task<ReadCharacterDTO> CreateCharacterAsync(CreateCharacterDTO character);
        public Task<bool> UpdateCharacterAsync(UpdateCharacterDTO character, int id);
        public Task<bool> DeleteCharacterAsync(int id);



    }
}
