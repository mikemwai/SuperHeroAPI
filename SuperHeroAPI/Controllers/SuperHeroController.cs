using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Data;
using SuperHeroAPI.Entities;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetAllHeroes()
        {
            //var heroes = new List<SuperHero>
            //{
            //    new SuperHero
            //    {
            //        Id = 1,
            //        Name = "Spiderman",
            //        FirstName = "Peter",
            //        LastName = "Parker",
            //        Place = "New York City"
            //    }
            //};
            
            var heroes = await _context.SuperHeroes.ToListAsync();

            return Ok(heroes);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> GetHero(int id)
        {            
            var hero = await _context.SuperHeroes.FindAsync(id);
            if(hero == null)
            {
                return BadRequest("Hero not found.");
            }

            return Ok(hero);
        }
        
        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {            
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }
    }
}
