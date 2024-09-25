using Lab_FastFoodTacoDB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lab_FastFoodTacoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrinksController : ControllerBase
    {
        LabTacosContext dbContext = new LabTacosContext();
        
        [HttpGet()]
        public IActionResult GetDrinks(string? SortByCost = "")
        {
            List<Drink> drinks = dbContext.Drinks.ToList();
            if (SortByCost.ToLower() == "ascending")
            {
                drinks = drinks.OrderBy(x => x.Cost).ToList();
            }
            else if (SortByCost.ToLower() == "descending")
            {
                 drinks = drinks.OrderByDescending(x => x.Cost).ToList();
            }
            return Ok(drinks);
        }
        [HttpGet("{id}")]
        public IActionResult GetDrinks(int id)
        {
            Drink drink = dbContext.Drinks.SingleOrDefault(x => x.Id == id);
            if (drink == null) { return NotFound("You can't drink that."); }
            else { return Ok(drink); }
        }
        [HttpPost()]
        public IActionResult CreateDrink(Drink newDrink)
        {
            newDrink.Id = 0;
            dbContext.Drinks.Add(newDrink);
            dbContext.SaveChanges();
            return Created($"/api/Drink/{newDrink.Id}", newDrink);
        }
        [HttpPut()]
        public IActionResult UpdateDrink(int id, [FromBody]Drink freshDrink)
        {
            if (id != freshDrink.Id) { return BadRequest("That's not a drink."); }
            if(dbContext.Drinks.Any(d => d.Id == id)==false) { return BadRequest(); }
            dbContext.Drinks.Update(freshDrink);
            dbContext.SaveChanges();
            return Ok(freshDrink);
        }

    }
}
