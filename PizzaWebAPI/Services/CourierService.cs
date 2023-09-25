using PizzaWebAPI.Core.Constants;
using PizzaWebAPI.Models;

namespace PizzaWebAPI.Services
{
    public class CourierService
    {
        private readonly PizzaDB _pizzaDB;

        public CourierService(PizzaDB pizzaDB)
        {
            _pizzaDB = pizzaDB;
        }

        public async Task<List<Courier>> GetAllCouriersAsync()
        {
            return await _pizzaDB.Couriers.ToListAsync();
        }

        public async Task<Courier> GetCourierAsync(int id)
        {
            return await _pizzaDB.Couriers.FindAsync(id);
        }

        public async Task<List<Courier>> GetAvailableCouriersAsync()
        {
            //var couriers = _pizzaDB.Couriers.Where(x => x.Status == (int)CourierStatus.Available).ToListAsync();

            var couriers = (from Couriers in _pizzaDB.Couriers
                            where Couriers.Status == (int)CourierStatus.Available
                            orderby Couriers.Rating
                            select Couriers).ToListAsync();

            return await couriers;
        }

        public async Task CreateCourierAsync(Courier courier)
        {
            _pizzaDB.Couriers.Add(courier);
            await _pizzaDB.SaveChangesAsync();
        }

        public async Task SaveCourierAsync()
        {
            await _pizzaDB.SaveChangesAsync();
        }

        public async Task DeleteCourierAsync(Courier courier)
        {
            _pizzaDB.Couriers.Remove(courier);
            await _pizzaDB.SaveChangesAsync();
        }

    }
}
