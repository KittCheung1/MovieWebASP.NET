using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using TestWebASP.NET.Data;
using TestWebASP.NET.DTO.Requests;
using TestWebASP.NET.DTO.Responses;
using TestWebASP.NET.Models;

namespace TestWebASP.NET.Controllers
{
    [Route("api/v1/characters")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]

    public class CharactersController : ControllerBase
    {

        private readonly ApplicationDbContext _dbcontext;
        private readonly IMapper _mapper;

        public CharactersController(ApplicationDbContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a character by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadCharacterDTO>> GetCharacter(int id)
        {
            var foundCharacter = await _dbcontext.Characters.FindAsync(id);


            if (foundCharacter == null)
            {
                return NotFound();
            }

            return _mapper.Map<ReadCharacterDTO>(foundCharacter);
        }

        /// <summary>
        /// Get all Characters
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<ReadCharacterDTO>> GetAllCharacters()
        {
            return _mapper.Map<List<ReadCharacterDTO>>(await _dbcontext.Characters.ToArrayAsync());
        }


        /// <summary>
        /// Add a new character
        /// </summary>
        /// <param name="createCharacter"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<CreateCharacterDTO>> CreateCharacter(CreateCharacterDTO createCharacter)
        {
            Character dbCharacter = _mapper.Map<Character>(createCharacter);
            _dbcontext.Characters.Add(dbCharacter);
            await _dbcontext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCharacter), new { dbCharacter.Id }, _mapper.Map<CreateCharacterDTO>(dbCharacter));
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
            if (id != updateCharacter.Id)
            {
                return BadRequest();
            }
            Character dbCharacter = _mapper.Map<Character>(updateCharacter);
            _dbcontext.Entry(dbCharacter).State = EntityState.Modified;

            try
            {
                await _dbcontext.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CharacterExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
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
            var foundCharacterToDelete = await _dbcontext.Characters.FindAsync(id);
            if (foundCharacterToDelete == null)
            {
                return NotFound();
            }

            _dbcontext.Characters.Remove(foundCharacterToDelete);
            await _dbcontext.SaveChangesAsync();

            return NoContent();
        }

        private bool CharacterExists(int id)
        {
            return _dbcontext.Characters.Any(e => e.Id == id);
        }
    }
}
