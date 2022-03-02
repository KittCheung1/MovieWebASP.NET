using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using TestWebASP.NET.Data;
using TestWebASP.NET.DTO.Franchise;
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
        /// Create a franchise
        /// </summary>
        /// <param name="franchise"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Franchise>> CreateFranchise(Franchise franchise)
        {
            _dbcontext.Franchises.Add(franchise);

            await _dbcontext.SaveChangesAsync();
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
