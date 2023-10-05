using PizzaWebAPI.Models;

namespace PizzaWebAPI.Services
{
    public class PizzasService
    {
        private readonly PizzaDB _pizzaDB;

        public PizzasService(PizzaDB pizzaDB)
        {
            _pizzaDB = pizzaDB;
        }

        public async Task<List<Pizzas>> GetAllPizzasAsync()
        {
            return await _pizzaDB.Pizzas.ToListAsync();
        }

        public async Task<Pizzas> GetPizzaAsync(int id)
        {
            return await _pizzaDB.Pizzas.FindAsync(id);
        }

        public async Task<List<Pizzas>> GetAvailablePizzasAsync()
        {
            var pizzas = (from p in _pizzaDB.Pizzas
                          where p.IsAvailable == true
                          select p).ToListAsync();
            return await pizzas;
        }
        public async Task CreatePizzaAsync(Pizzas pizza)
        {
            _pizzaDB.Pizzas.Add(pizza);
            await _pizzaDB.SaveChangesAsync();
        }

        public async Task SavePizzaAsync()
        {
            await _pizzaDB.SaveChangesAsync();
        }

        public async Task DeletePizzaAsync(Pizzas pizza)
        {
            _pizzaDB.Pizzas.Remove(pizza);
            await _pizzaDB.SaveChangesAsync();
        }
    }
}
