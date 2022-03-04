using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using TestWebASP.NET.DTO.Franchise;
using TestWebASP.NET.DTO.Responses;
using TestWebASP.NET.Models;
using TestWebASP.NET.Services;

namespace TestWebASP.NET.Controllers
{
    [Route("api/v1/franchises")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class FranchisesController : ControllerBase
    {
        private readonly IFranchiseService _franchiseService;

        public FranchisesController(IFranchiseService franchiseService)
        {
            _franchiseService = franchiseService;
        }
        /// <summary>
        /// Get all franchises
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Task<IEnumerable<ReadFranchiseDTO>> GetAllFranchises()
        {
            return _franchiseService.GetAllFranchisesAsync();
        }

        /// <summary>
        /// Get a franchise by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadFranchiseDTO>> GetFranchise(int id)
        {
            var foundFranchise = await _franchiseService.GetFranchiseAsync(id);


            if (foundFranchise == null)
            {
                return NotFound();
            }

            return foundFranchise;
        }

        /// <summary>
        /// Get all movies from a franchise id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("movies/{id}")]
        public Task<IEnumerable<ReadMovieDTO>> GetAllMoviesInFranchise(int id)
        {
            return _franchiseService.GetAllMoviesInFranchiseAsync(id);

        }

        /// <summary>
        /// Get all characters in franchise by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/characters")]
        public Task<IEnumerable<ReadCharacterDTO>> GetAllCharactersInFranchise(int id)
        {
            return _franchiseService.GetAllCharactersInFranchiseAsync(id);
        }

        /// <summary>
        /// Update a franchise by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateFranchise"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFranchise(int id, UpdateFranchiseDTO updateFranchise)
        {
            bool franchiseExists = await _franchiseService.UpdateFranchiseAsync(updateFranchise, id);

            if (!franchiseExists)
            {
                return NotFound();
            }
            return NoContent();
        }

        /// <summary>
        /// Put movies in Franchise by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="movieIds"></param>
        /// <returns></returns>
        [HttpPut("movie/{id}")]
        public async Task<IActionResult> UpdateMovieInFranchise(int id, [FromBody] List<int> movieIds)
        {
            await _franchiseService.UpdateMovieInFranchiseAsync(movieIds, id);
            return Ok();
        }

        /// <summary>
        /// Create a franchise
        /// </summary>
        /// <param name="createFranchise"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Franchise>> CreateFranchise(CreateFranchiseDTO createFranchise)
        {

            var franchise = await _franchiseService.CreateFranchiseAsync(createFranchise);

            return CreatedAtAction(nameof(GetFranchise), new { franchise.Id }, franchise);
        }

        /// <summary>
        /// Delete a franchise by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFranchise(int id)
        {
            bool franchiseExists = await _franchiseService.DeleteFranchiseAsync(id);

            if (!franchiseExists)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
