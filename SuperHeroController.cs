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
            var heroes = await _context.SuperHeroes.ToListAsync();
          
            return Ok(heroes);
        }
        [HttpGet]
        [Route("{Id}")]
        public async Task<ActionResult<SuperHero>> GetHero(int Id)
        {
            var hero = await _context.SuperHeroes.FindAsync(Id);
            if (hero == null)
            {
                return NotFound("Hero Not Found!");
            }

            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero([FromBody]SuperHero hero)
        {
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }
        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero( SuperHero updatedhero)
        {
            var DbHero = await _context.SuperHeroes.FindAsync(updatedhero.Id);
            if (DbHero == null)
            {
                return NotFound("Hero Not Found!"); 
            }
                DbHero.Name = updatedhero.Name;
                DbHero.FirstName = updatedhero.FirstName;
                DbHero.LastName = updatedhero.LastName;
                DbHero.Place = updatedhero.Place;
                await _context.SaveChangesAsync();
           

            return Ok(await _context.SuperHeroes.ToListAsync());
        }
        [HttpDelete]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int Id)
        {
            var DbHero = await _context.SuperHeroes.FindAsync(Id);
            if (DbHero == null)
            {
                return NotFound("Hero Not Found!");
            }
            _context.SuperHeroes.Remove(DbHero);


            return Ok(await _context.SuperHeroes.ToListAsync());
        }
    }
}
