using Lab_FastFoodTacoDB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lab_FastFoodTacoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TacosController : ControllerBase
    {
        LabTacosContext dbContext = new LabTacosContext();

        [HttpGet()]
        public IActionResult GetTacos(bool? softshell = null)
        {
            List<Taco> tacos = dbContext.Tacos.ToList();
            if (softshell!=null) 
            {
                tacos = tacos.Where(t => t.SoftShell == softshell).ToList();
            }
            return Ok(tacos);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Taco taco = dbContext.Tacos.SingleOrDefault(t => t.Id == id);
            if (taco == null) { return NotFound("That's not a taco..."); }
            else { return Ok(taco); }
        }

        [HttpPost()]
        public IActionResult CreateTaco([FromBody] Taco customTaco)
        {
            customTaco.Id = 0;
            dbContext.Tacos.Add(customTaco);
            dbContext.SaveChanges();
            return Created($"/api/Taco/{customTaco.Id}", customTaco);
        }

        [HttpDelete()]
        public IActionResult DeleteTaco(int id)
        {
            Taco t = dbContext.Tacos.FirstOrDefault(t => t.Id == id);
            if (t == null) { return NotFound("That's a hardshell to crack"); }
            else
            {
                dbContext.Tacos.Remove(t);
                dbContext.SaveChanges();
                return NoContent();
            }

        }
    }
}
