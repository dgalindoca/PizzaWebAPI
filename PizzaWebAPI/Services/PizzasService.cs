using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<Pizzas>> GetAllPizzas()
        {
            return await _pizzaDB.Pizzas.ToListAsync();
        }

        public async Task<Pizzas> GetPizza(int id)
        {
            return await _pizzaDB.Pizzas.FindAsync(id);
        }


        public async Task CreatePizza(Pizzas pizza)
        {
            _pizzaDB.Pizzas.Add(pizza);
            await _pizzaDB.SaveChangesAsync();
        }

        public async Task SavePizza()
        {
            await _pizzaDB.SaveChangesAsync();
        }

        public async Task DeletePizza(Pizzas pizza)
        {
            _pizzaDB.Pizzas.Remove(pizza);
            await _pizzaDB.SaveChangesAsync();
        }
    }
}
