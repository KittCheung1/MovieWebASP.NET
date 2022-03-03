using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using TestWebASP.NET.Data;
using TestWebASP.NET.DTO.Franchise;
using TestWebASP.NET.DTO.Responses;
using TestWebASP.NET.Models;

namespace TestWebASP.NET.Controllers
{
    [Route("api/v1/franchises")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class FranchisesController : ControllerBase
    {
        private readonly ApplicationDbContext _dbcontext;
        private readonly IMapper _mapper;

        public FranchisesController(ApplicationDbContext context, IMapper mapper)
        {
            _dbcontext = context;
            _mapper = mapper;
        }
        /// <summary>
        /// Get all franchises
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadFranchiseDTO>>> GetFranchises()
        {
            return _mapper.Map<List<ReadFranchiseDTO>>(await _dbcontext.Franchises.ToArrayAsync());
        }

        /// <summary>
        /// Get a franchise by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadFranchiseDTO>> GetFranchise(int id)
        {
            var foundFranchise = await _dbcontext.Franchises.FindAsync(id);


            if (foundFranchise == null)
            {
                return NotFound();
            }

            return _mapper.Map<ReadFranchiseDTO>(foundFranchise);
        }

        /// <summary>
        /// Get all movies from a franchise id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("movies/{id}")]
        public async Task<ActionResult<List<ReadMovieDTO>>> GetAllMoviesInFranchise(int id)
        {
            var dbFranchise = await _dbcontext.Franchises.Include(f => f.Movies).FirstOrDefaultAsync(f => f.Id == id);

            if (dbFranchise == null)
            {
                return NotFound();
            }

            return _mapper.Map<List<ReadMovieDTO>>(dbFranchise.Movies.ToList());
        }

        /// <summary>
        /// Get all characters in franchise by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[HttpGet("characters/{id}")]
        //public async Task<ActionResult<List<ReadCharacterDTO>>> GetAllCharactersInFranchise(int id, Movie movieId)
        //{
        //    var dbFranchise = await _dbcontext.Franchises.Include(f => f.Movies).FirstOrDefaultAsync(f => f.Id == id);
        //    var dbMovie = await _dbcontext.Movies.Include(m=> m.Characters).FirstOrDefaultAsync(m => m.Id == movieId);

        //    if (dbFranchise == null)
        //    {
        //        return NotFound();
        //    }

        //    return _mapper.Map<List<ReadMovieDTO>>(dbFranchise.Movies.ToList());
        //}

        /// <summary>
        /// Update a franchise by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateFranchise"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFranchise(int id, UpdateFranchiseDTO updateFranchise)
        {
            if (id != updateFranchise.Id)
            {
                return BadRequest();
            }
            Franchise dbFranchise = _mapper.Map<Franchise>(updateFranchise);
            _dbcontext.Entry(dbFranchise).State = EntityState.Modified;
            try
            {
                await _dbcontext.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FranchiseExists(id))
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
        /// Add / Update a movie in Franchise
        /// </summary>
        /// <param name="id"></param>
        /// <param name="movieId"></param>
        /// <returns></returns>
        [HttpPut("movie/{id}")]
        public async Task<IActionResult> UpdateMovieInFranchise(int id, [FromBody] int movieId)
        {
            var dbFranchise = await _dbcontext.Franchises.Include(f => f.Movies).FirstOrDefaultAsync(f => f.Id == id);
            var movie = await _dbcontext.Movies.FindAsync(movieId);
            if (dbFranchise == null)
            {
                return NotFound();
            }
            dbFranchise.Movies.Add(movie);
            await _dbcontext.SaveChangesAsync();
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
            Franchise dbFranchise = _mapper.Map<Franchise>(createFranchise);
            _dbcontext.Franchises.Add(dbFranchise);
            await _dbcontext.SaveChangesAsync();


            return CreatedAtAction(nameof(GetFranchise), new { dbFranchise.Id }, _mapper.Map<CreateFranchiseDTO>(dbFranchise));
        }

        /// <summary>
        /// Delete a franchise by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFranchise(int id)
        {
            var franchise = await _dbcontext.Franchises.FindAsync(id);
            if (franchise == null)
            {
                return NotFound();
            }

            _dbcontext.Franchises.Remove(franchise);
            await _dbcontext.SaveChangesAsync();

            return NoContent();
        }

        private bool FranchiseExists(int id)
        {
            return _dbcontext.Franchises.Any(e => e.Id == id);
        }
    }
}
