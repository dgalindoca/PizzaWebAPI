using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaWebAPI.Core.Constants;
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
                List<Pizzas> pizzas = await _pizzasService.GetAllPizzasAsync();
                return pizzas == null || pizzas.Count == 0 ? BadRequest( new 
                { 
                    error = "No pizzas found" 
                }) : Ok(pizzas);
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
                Pizzas pizza = await _pizzasService.GetPizzaAsync(id);
                return pizza == null ? BadRequest( new
                {
                    error = "Pizza not found"
                }) : Ok(pizza);
            }catch(Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }

        [HttpGet("Available")]
        public async Task<IActionResult> GetAvailablePizzas()
        {
            try
            {
                List<Pizzas> pizzas = await _pizzasService.GetAvailablePizzasAsync();
                return pizzas == null || pizzas.Count == 0 ? BadRequest(new
                {
                    error = "No Available pizzas found"
                }) : Ok(pizzas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message} );
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePizza([FromBody]Pizzas pizza)
        {
            try
            {
                //if (!Enum.IsDefined(typeof(PizzasSizes), pizza.PizzaSize)) { return BadRequest("The size of the pizza is not valid"); }
                await _pizzasService.CreatePizzaAsync(pizza);
                return Ok(await _pizzasService.GetAllPizzasAsync());
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
                Pizzas pizza = await _pizzasService.GetPizzaAsync(id);
                if (pizza == null) { return BadRequest( new
                {
                    error = "Pizza not found"
                }); }

                //if (!Enum.IsDefined(typeof(PizzasSizes), pizza.PizzaSize)) { return BadRequest("The size of the pizza is not valid"); }
                pizza.PizzaName = request.PizzaName;
                pizza.PizzaDescription = request.PizzaDescription;
                pizza.ImageUrl = request.ImageUrl;
                //pizza.PizzaSize = request.PizzaSize;
                pizza.Price = request.Price;
                pizza.IsAvailable = request.IsAvailable;

                await _pizzasService.SavePizzaAsync();

                return Ok(await _pizzasService.GetAllPizzasAsync());
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
                Pizzas pizza = await _pizzasService.GetPizzaAsync(id);
                if (pizza == null) { return BadRequest( new
                {
                    error = "Pizza not found"
                }); }

                await _pizzasService.DeletePizzaAsync(pizza);
                return Ok(await _pizzasService.GetAllPizzasAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {ex.Message});
            }
        }

    }
}
