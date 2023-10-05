using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaWebAPI.Core.Constants;
using PizzaWebAPI.Models;
using PizzaWebAPI.Services;

namespace PizzaWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourierController : ControllerBase
    {
        private readonly CourierService _courierService;

        public CourierController(CourierService courierService)
        {
            _courierService = courierService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCouriers()
        {
            try
            {
                List<Courier> couriers = await _courierService.GetAllCouriersAsync();
                if (couriers == null || couriers.Count == 0) { return BadRequest( new
                {
                    error = "No couriers found"
                }); }
                return Ok(couriers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourier(int id)
        {
            try
            {
                Courier courier = await _courierService.GetCourierAsync(id);
                return courier == null ? BadRequest(new
                {
                    error = "Courier not found"
                }) : Ok(courier);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message  });
            }
        }

        [HttpGet("Available")]
        public async Task<IActionResult> GetAvailableCouriers()
        {
            try
            {
                List<Courier> couriers = await _courierService.GetAvailableCouriersAsync();
                return couriers == null || couriers.Count == 0 ? BadRequest( new
                {
                    error = "There are no available couriers"
                }) : Ok(couriers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourier([FromBody] Courier courier)
        {
            try
            {
                if (!Enum.IsDefined(typeof(CourierStatus), courier.Status)) { return BadRequest( new
                {
                    error = "The status of the courier is not valid"
                }); }
                await _courierService.CreateCourierAsync(courier);
                return Ok(await _courierService.GetAllCouriersAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourier(int id, [FromBody] Courier request)
        {
            try
            {
                Courier courier = await _courierService.GetCourierAsync(id);
                if (courier == null) { return BadRequest(new
                {
                    error = "Courier not found"
                }); }

                courier.CourierName = request.CourierName;
                courier.Deliveries = request.Deliveries;
                courier.Rating = request.Rating;
                courier.Phone = request.Phone;
                courier.Status = request.Status;
                await _courierService.SaveCourierAsync();

                return Ok(await _courierService.GetAllCouriersAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourier(int id)
        {
            try
            {
                Courier courier = await _courierService.GetCourierAsync(id);
                if (courier == null) { return BadRequest(new
                {
                    error = "Courier not found"
                }); }

                await _courierService.DeleteCourierAsync(courier);
                return Ok(await _courierService.GetAllCouriersAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
