using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaWebAPI.Models
{
    [Table("pizzas")]
    public class Pizzas
    {
        public int Id { get; set; }
        public string PizzaName { get; set; } = string.Empty;
        public string PizzaDescription { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public double Price { get; set; }
        public bool IsAvailable{ get; set; }
    }
}
