using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWebASP.NET.Data;
using TestWebASP.NET.DTO.Requests;
using TestWebASP.NET.DTO.Responses;
using TestWebASP.NET.Models;

namespace TestWebASP.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class CharactersController : ControllerBase
    {

        private readonly ApplicationDbContext _dbcontext;

        public CharactersController(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterResponse>> GetCharacter(int id)
        {
            var foundCharacter = await _dbcontext.Characters.FindAsync(id);


            if (foundCharacter == null)
            {
                return NotFound();
            }

            return MapToCharacterResponse(foundCharacter);
        }

        [HttpGet]
        public async Task<IEnumerable<CharacterResponse>> GetAllCharacter()
        {
            var foundCharacters = await _dbcontext.Characters.ToArrayAsync();

            return foundCharacters.Select(MapToCharacterResponse);
        }

        [HttpPost]
        public async Task<ActionResult<CharacterResponse>> CreateCharacter(CreateCharacterRequest createCharacterRequest)
        {

            var character = new Character()
            {
                FullName = createCharacterRequest.FullName,
                Alias = createCharacterRequest.Alias,
                Gender = createCharacterRequest.Gender,
                Picture = createCharacterRequest.Picture
            };

            _dbcontext.Characters.Add(character);

            await _dbcontext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCharacter), new { character.Id }, MapToCharacterResponse(character));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCharacter(UpdateCharacterRequest updateCharacterRequest, int id)
        {
            var foundUpdateCharacter = await _dbcontext.Characters.FindAsync(id);

            if (foundUpdateCharacter == null)
                return NotFound();

            if (updateCharacterRequest.FullName != null)
                foundUpdateCharacter.FullName = updateCharacterRequest.FullName;

            if (updateCharacterRequest.Alias != null)
                foundUpdateCharacter.Alias = updateCharacterRequest.Alias;

            if (updateCharacterRequest.Gender != null)
                foundUpdateCharacter.Gender = updateCharacterRequest.Gender;

            if (updateCharacterRequest.Picture != null)
                foundUpdateCharacter.Picture = updateCharacterRequest.Picture;

            _dbcontext.Characters.Update(foundUpdateCharacter);
            await _dbcontext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCharacter(int id)
        {
            var foundCharacterToDelete = await _dbcontext.Characters.FindAsync(id);
            if (foundCharacterToDelete == null)
            {
                return NotFound();
            }

            _dbcontext.Characters.Remove(foundCharacterToDelete);
            await _dbcontext.SaveChangesAsync();

            return NoContent();
        }

        private static CharacterResponse MapToCharacterResponse(Character character)
        {
            return new CharacterResponse()
            {
                Id = character.Id,
                FullName = character.FullName,
                Alias = character.Alias,
                Gender = character.Gender,
                Picture = character.Picture
            };
        }
    }
}
