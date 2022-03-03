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
    [Route("api/v1/movies")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _dbcontext;
        private readonly IMapper _mapper;

        public MoviesController(ApplicationDbContext context, IMapper mapper)
        {
            _dbcontext = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all movies
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<ReadMovieDTO>> GetMovies()
        {
            return _mapper.Map<List<ReadMovieDTO>>(await _dbcontext.Movies.ToArrayAsync());
        }

        /// <summary>
        /// Get a movie by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadMovieDTO>> GetMovie(int id)
        {
            var foundMovie = await _dbcontext.Movies.FindAsync(id);

            if (foundMovie == null)
            {
                return NotFound();
            }
            return _mapper.Map<ReadMovieDTO>(foundMovie);
        }

        /// <summary>
        /// Update a movie by id
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="updateMovie"></param>
        /// <returns></returns>
        [HttpPut("{movieId}")]
        public async Task<IActionResult> UpdateMovie(int movieId, UpdateMovieDTO updateMovie)
        {
            if (movieId != updateMovie.Id)
            {
                return BadRequest();
            }

            // Map to domain
            Movie dbMovie = _mapper.Map<Movie>(updateMovie);
            _dbcontext.Entry(dbMovie).State = EntityState.Modified;

            try
            {
                await _dbcontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(movieId))
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
        /// Create a movie
        /// </summary>
        /// <param name="movie"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Movie>> CreateMovie(CreateMovieDTO createMovie)
        {
            Movie dbMovie = _mapper.Map<Movie>(createMovie);
            _dbcontext.Movies.Add(dbMovie);
            await _dbcontext.SaveChangesAsync();

            return CreatedAtAction("GetMovie", new { dbMovie.Id }, _mapper.Map<CreateMovieDTO>(dbMovie));
        }

        /// <summary>
        /// Delete a movie by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _dbcontext.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _dbcontext.Movies.Remove(movie);
            await _dbcontext.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(int id)
        {
            return _dbcontext.Movies.Any(e => e.Id == id);
        }
    }
}
