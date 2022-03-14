using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ContosoPizzaNew.Services;
using ContosoPizzaNew.Models;
namespace ContosoPizzaNew.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PizzaController : ControllerBase
    {
        public PizzaController()
        {
        }

        [HttpGet]
        public ActionResult<List<Pizza>> GetAll() => PizzaService.GetAll();

        [HttpGet("{id}")]
        public ActionResult<Pizza> Get(int id)
        {
            var pizza = PizzaService.Get(id);

            if (pizza == null)
                return NotFound();

            return pizza;
        }

        [HttpPost]
        public IActionResult Create(Pizza pizza)
        {
            PizzaService.Add(pizza);

            return Created($"/Pizza/{pizza.Id}", pizza);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Pizza pizza)
        {

            if (id != pizza.Id)
                return BadRequest();

            var pizzaDetails = PizzaService.Get(id);

            if (pizzaDetails == null)
                return NotFound();

            pizzaDetails.Name = pizza.Name;
            pizzaDetails.IsGlutenFree = pizza.IsGlutenFree;

            PizzaService.Update(pizzaDetails);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var pizza = PizzaService.Get(id);

            if (pizza == null)
                return NotFound();

            PizzaService.Delete(id);

            return Ok();
        }
    }
}
