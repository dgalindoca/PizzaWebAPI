using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaWebAPI.Models
{
    [Table("pizzasOrders")]
    public class PizzasOrders
    {
        [Key]
        public int Id { get; set; }
        public int PizzaId { get; set; }
        public int OrderId { get; set; }
        public int AmountOfPizzas { get; set; }
        public double Price { get; set; }
    }
}
