using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaWebAPI.Models;
using PizzaWebAPI.Services;

namespace PizzaWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzasController : ControllerBase
    {
        private readonly PizzasService _pizzasService;

        public PizzasController(PizzasService pizzasService)
        {
            _pizzasService = pizzasService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPizzas()
        {
            try
            {
                List<Pizzas> pizzas = await _pizzasService.GetAllPizzas();
                if (pizzas == null || pizzas.Count == 0) { return BadRequest("No pizzas found"); }
                return Ok(pizzas);
            }catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPizza(int id)
        {
            try
            {
                Pizzas pizza = await _pizzasService.GetPizza(id);
                if (pizza == null) { return BadRequest("Pizza not found"); }
                return Ok(pizza);
            }catch(Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePizza([FromBody]Pizzas pizza)
        {
            try
            {
                await _pizzasService.CreatePizza(pizza);
                return Ok(await _pizzasService.GetAllPizzas());
            }catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePizza(int id, Pizzas request)
        {
            try
            {
                Pizzas pizza = await _pizzasService.GetPizza(id);
                if (pizza == null) { return BadRequest("Pizza not found"); }

                pizza.PizzaName = request.PizzaName;
                pizza.PizzaDescription = request.PizzaDescription;
                pizza.ImageUrl = request.ImageUrl;
                pizza.PizzaSize = request.PizzaSize;
                pizza.Price = request.Price;
                pizza.Stock = request.Stock;

                await _pizzasService.SavePizza();

                return Ok(await _pizzasService.GetAllPizzas());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePizza(int id)
        {
            try
            {
                Pizzas pizza = await _pizzasService.GetPizza(id);
                if (pizza == null) { return BadRequest("Pizza not found"); }
                await _pizzasService.DeletePizza(pizza);
                return Ok(await _pizzasService.GetAllPizzas());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {ex.Message});
            }
        }

    }
}
