using Microsoft.EntityFrameworkCore;
using PizzaWebAPI.Models;

namespace PizzaWebAPI.Data
{
    public class PizzaDB : DbContext
    {

        public PizzaDB(DbContextOptions<PizzaDB> options) : base(options) { }

        public DbSet<Users> Users => Set<Users>();
        public DbSet<Pizzas> Pizzas => Set<Pizzas>();
        public DbSet<Orders> Orders => Set<Orders>();
        public DbSet<PizzasOrders> PizzasOrders => Set<PizzasOrders>();
        public DbSet<Courier> Couriers => Set<Courier>();
    }
}
