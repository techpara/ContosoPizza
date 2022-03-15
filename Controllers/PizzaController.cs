using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ContosoPizzaNew.Services;
using ContosoPizzaNew.Models;
using Microsoft.AspNetCore.Diagnostics;

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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Pizza))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<Pizza>> GetAll()
        {
            throw new Exception("Sample exception.");
            PizzaService.GetAll();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Pizza))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Pizza> Get(int id)
        {
            //throw new Exception("Sample exception.");
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
            throw new Exception("Sample exception.");
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

        [Route("/error-development")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult HandleErrorDevelopment([FromServices] IHostEnvironment hostEnvironment)
        {
            if (!hostEnvironment.IsDevelopment())
            {
                return NotFound();
            }

            var exceptionHandlerFeature =
                HttpContext.Features.Get<IExceptionHandlerFeature>()!;

            return Problem(
                detail: exceptionHandlerFeature.Error.StackTrace,
                title: exceptionHandlerFeature.Error.Message);
        }

        [Route("/error")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult HandleError() =>
         Problem();
    }
}
