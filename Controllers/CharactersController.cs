using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using TestWebASP.NET.DTO.Requests;
using TestWebASP.NET.DTO.Responses;
using TestWebASP.NET.Services;

namespace TestWebASP.NET.Controllers
{
    [Route("api/v1/characters")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]

    public class CharactersController : ControllerBase
    {

        private readonly ICharacterService _characterService;

        public CharactersController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        /// <summary>
        /// Get a character by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadCharacterDTO>> GetCharacter(int id)
        {
            var foundCharacter = await _characterService.GetCharacterAsync(id);

            if (foundCharacter == null)
            {
                return NotFound();
            }

            return foundCharacter;
        }

        /// <summary>
        /// Get all Characters
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Task<IEnumerable<ReadCharacterDTO>> GetAllCharacters()
        {
            return _characterService.GetAllCharactersAsync();
        }


        /// <summary>
        /// Add a new character
        /// </summary>
        /// <param name="createCharacter"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<CreateCharacterDTO>> CreateCharacter(CreateCharacterDTO createCharacter)
        {
            var character = await _characterService.CreateCharacterAsync(createCharacter);

            return CreatedAtAction(nameof(GetCharacter), new { character.Id }, character);
        }

        /// <summary>
        /// Update Character by id
        /// </summary>
        /// <param name="updateCharacter"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCharacter(UpdateCharacterDTO updateCharacter, int id)
        {
            bool characterExists = await _characterService.UpdateCharacterAsync(updateCharacter, id);

            if (!characterExists)
            {
                return NotFound();
            }
            return NoContent();
        }

        /// <summary>
        /// Delete a character by id
        /// </summary>
        /// <param name="id"></param> 
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCharacter(int id)
        {
            bool characterExists = await _characterService.DeleteCharacterAsync(id);

            if (!characterExists)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
